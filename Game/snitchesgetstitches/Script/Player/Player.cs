using Godot;
using System;

public partial class Player : CharacterBody2D
{
	#region Variables
	public bool PlayerHardMode = false;
	[Export] public bool invincible = false;
	public bool CanMove = true;
	public int baseHealth = 3;
	[Export] float JumpStrength = -2500; //was -1500
	[Export] float initialGravity = 3000;
	float Gravity = 3000;
	[Export] float gravityMultiplier = 1;
	#endregion
	bool isCrouching = false;


	[Export] GameManager gameManager;
	[Export] float maxJumpHeight = 200;
	[Export] public Vector2 startingPostion = new Vector2(101, 542);
	[Export] AnimatedSprite2D RunningSprite;
	[Export] Area2D StandingHitBox;
	[Export] AnimatedSprite2D CrounchingSprite;
	[Export] Area2D CrounchingHitBox;
	[Export] AnimationPlayer animationPlayer;
	[Export] Node2D SpeechBubble;
	[Export] HeartContainer heartContainer;
	private AnimatedSprite2D calvin;
	private AnimatedSprite2D slide;
	public override void _Ready()
	{
		// Just checks if any of the nonsense that I made is actually there
		calvin = GetNode<AnimatedSprite2D>("Running/Calvin");
		if (calvin == null){
			GD.Print("Ain't no Calvin!");
		}
		slide = GetNode<AnimatedSprite2D>("Crouching/PlayerCrounchSprite");
		if (slide == null){
			GD.Print("We ain't sliding!");
		}
		CrounchingSprite.Visible = false;
		heartContainer.SetHearts(baseHealth);
	}

	public override void _PhysicsProcess(double delta)
	{
		
		if(CanMove)
		{
			if(Input.IsActionPressed("Crouch") && Position.Y == startingPostion.Y && !isCrouching)
			{
				Crouch();
				isCrouching = true;
			}
			
			if(Input.IsActionJustReleased("Crouch"))
			{
				//Stand();
				isCrouching = false;
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
				//GD.Print("Jumping");
				
				Stand();
				velocity.Y = JumpStrength; // Ensure jump applies correctly
				calvin.Play("jump");  // Plays the Jump Animation.
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
			//More Nnamdi Code
			// If the player 'hits the floor', stop playing the jump animation.
			if(Position.Y == startingPostion.Y &&  velocity.Y >= 0 && calvin.Animation == "jump")
			{
				calvin.Play("default");
			}
			Velocity = velocity;
			MoveAndSlide();
		}
	}	
	private void Crouch()
	{
		
		if(PlayerHardMode)
		{
			/* Nnamdi was here!!!
			calvin.Play("slide");
			calvin.AnimationFinished += WeSlidingNow;
			*/
			slide.Play("slide");
			slide.Frame = 1;
			animationPlayer.Play("Crouch");


		}
		else
		{
			slide.Play("slide");
			slide.Frame = 1;
			animationPlayer.Play("CrouchNM");
		}
		
		
	}

 /* More Nnamdi code
	private void WeSlidingNow(){
		calvin.Play("default"); // return to running
		calvin.AnimationFinished -= WeSlidingNow; // Ends the slide animation
	}
*/
	private void Stand()
	{
		RunningSprite.Visible = true;
		CrounchingSprite.Visible = false;
		//Hitboxes
		StandingHitBox.Monitorable = true;
		CrounchingHitBox.Monitorable = false;
	}

	public void PlayerTakesDamage(int DamageAmount)
	{
		if(invincible)
		{
			return;
		}
		
		baseHealth -= DamageAmount;
		heartContainer.RemoveHearts(DamageAmount);
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
	public void WalkTeacherAnimation()
	{
		animationPlayer.Play("WalkToTeacher");
		SpeechBubble.Visible = true;

	}
		
}
