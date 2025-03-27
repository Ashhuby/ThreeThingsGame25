using Godot;
using System;

public partial class Player : CharacterBody2D
{
	#region Variables
	public bool CanMove = true;
	public int baseHealth = 3;
	[Export] float JumpStrength = -1500;
	[Export] float initialGravity = 3000;
	float Gravity = 3000;
	[Export] float gravityMultiplier = 1;
	#endregion

	[Export] GameManager gameManager;
	[Export] float maxJumpHeight = 200;
	[Export] Vector2 startingPostion = new Vector2(101, 542);
	[Export] Sprite2D StandingSprite;
	[Export] Area2D StandingHitBox;
	[Export] Sprite2D CrounchingSprite;
	[Export] Area2D CrounchingHitBox;
	public override void _Ready()
	{
		StandingSprite.Visible = true;
		CrounchingSprite.Visible = false;
	}

	public override void _PhysicsProcess(double delta)
	{
		
		if(CanMove)
		{
			if(Input.IsActionPressed("Crouch") && Position.Y == startingPostion.Y)
			{
				Crouch();
			}
			if(Input.IsActionJustReleased("Crouch"))
			{
				Stand();
			}

			
			Vector2 velocity = Velocity;

			if(Position.Y == startingPostion.Y)
			{
				gravityMultiplier = 1;
				Gravity = 0;
			}
			else
			{
				Gravity = initialGravity;
			}

			//Apply gravity
			velocity.Y += (float)delta * Gravity * gravityMultiplier; 

			//Handle jumping
			if(Input.IsActionJustPressed("Jump") && Position.Y == startingPostion.Y)
			{
				GD.Print("Jumping");
				Stand();
				velocity.Y = JumpStrength; // Ensure jump applies correctly
			}

			//Prevent player from falling below the starting position
			if (Position.Y > startingPostion.Y)
			{
				Position = new Vector2(Position.X, startingPostion.Y);
				velocity.Y = 0; //Stop downward movement
			}

			// Prevent player from jumping too high
			if (Position.Y < maxJumpHeight)
			{
				Position = new Vector2(Position.X, maxJumpHeight);
				velocity.Y = 0; // Stop upward movement
				gravityMultiplier = 1.5f;
			}

			Velocity = velocity;
			MoveAndSlide();
		}
	}	
	private void Crouch()
	{
		//Sprites
		StandingSprite.Visible = false;
		CrounchingSprite.Visible = true;
		//Hitboxes
		StandingHitBox.Monitorable = false;
		CrounchingHitBox.Monitorable = true;
	}
	private void Stand()
	{
		StandingSprite.Visible = true;
		CrounchingSprite.Visible = false;
		//Hitboxes
		StandingHitBox.Monitorable = true;
		CrounchingHitBox.Monitorable = false;
	}

	public void PlayerTakesDamage(int DamageAmount)
	{
		baseHealth -= DamageAmount;
		gameManager.UpdateHealth(baseHealth);
		//GD.Print("Player Health: " + baseHealth);
		if(baseHealth <= 0)
		{
			Die();
		}
	}
	private void Die()
	{
		//QueueFree();
		GD.Print("Player has died");	
		gameManager.IsGameOver = true;
	}
		
}
