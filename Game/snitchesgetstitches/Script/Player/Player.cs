using Godot;
using System;
using System.Threading.Tasks;

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
	[Export] EndlessGameManager endlessGameManager;
	[Export] float maxJumpHeight = 200;
	[Export] public Vector2 startingPostion = new Vector2(101, 542);
	[Export] AnimatedSprite2D RunningSprite;
	[Export] Area2D StandingHitBox;
	[Export] AnimatedSprite2D CrounchingSprite;
	[Export] Area2D CrounchingHitBox;
	[Export] AnimationPlayer animationPlayer;
	[Export] Node2D SpeechBubble;
	[Export] HeartContainer heartContainer;
	[Export]private AnimatedSprite2D calvin;
	[Export]private AnimatedSprite2D slide;

	#region SFX
	[Export] AudioStreamPlayer2D jumpSFX;
	[Export] AudioStreamPlayer2D crouchSFX;
	[Export] AudioStreamPlayer2D runningSFX;
	[Export] AudioStreamPlayer2D GetHitSFX;
	[Export] AudioStreamPlayer2D GetHomeworkSFX;
	[Export] AudioStreamPlayer2D GlassBreakSFX;
	#endregion

	public override void _Ready()
	{
		/*
		// Just checks if any of the nonsense that I made is actually there
		calvin = GetNode<AnimatedSprite2D>("Running/Calvin");
		if (calvin == null){
			GD.Print("Ain't no Calvin!");
		}
		slide = GetNode<AnimatedSprite2D>("Crouching/PlayerCrounchSprite");
		if (slide == null){
			GD.Print("We ain't sliding!");
		}
		*/
		CrounchingSprite.Visible = false;
		heartContainer.SetHearts(baseHealth);
	}

	public override void _PhysicsProcess(double delta)
	{
		runningSFX.Play();
		if(CanMove)
		{
			if(Input.IsActionPressed("Crouch") && Position.Y == startingPostion.Y && !isCrouching)
			{
				runningSFX.Stop();
				crouchSFX.Play();
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
				runningSFX.Stop();
				jumpSFX.Play();
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
		CrounchingHitBox.Monitorable = true;
		
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
		gameManager?.UpdateHealth(baseHealth);
		endlessGameManager?.EndlessUpdateHealth(baseHealth);
		//GD.Print("Player Health: " + baseHealth);
		if(baseHealth <= 0)
		{
			Die();
		}
	}
	private void Die()
	{
		//QueueFree();
		GlassBreakSFX.Play();
		invincible = true;
		GD.Print("Player has died");	
		if(gameManager != null) gameManager.IsGameOver = true;
		if(endlessGameManager != null) endlessGameManager.EndlessIsGameOver = true;
		calvin.Play("dead");
		calvin.Frame = 1;
	}
	public async Task WalkTeacherAnimation()
	{
		animationPlayer.Play("WalkToTeacher");
		SpeechBubble.Visible = true;
		// Wait for 1.2 seconds before playing "halt"
		await Task.Delay(1600);
		calvin.Play("halt");
		calvin.Frame = 1;

	}
	public void HideHearts()
	{
		heartContainer.Visible = false;
	}
	public void PlayHitSound()
	{
		if(invincible) return;
		GetHitSFX.PitchScale = (float)GD.RandRange(0.8f,1.2f);
		GetHitSFX.Play(0.2f);
	}
	public void PlayHomeworkSound()
	{
		if(invincible) return;
		GetHomeworkSFX.PitchScale = (float)GD.RandRange(0.9f,1.4f);
		GetHomeworkSFX.Play(0.6f);
	}
		
}
