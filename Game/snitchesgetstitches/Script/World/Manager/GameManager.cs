using Godot;
using System;

public partial class GameManager : Node2D
{
	[Export] Label scoreLabel;
	[Export] Label healthLabel;
	[Export] Player player;
	[Export] GameOverScreen gameOverScreen;
	[Export] WinScreen winScreen;
	[Export] ObjectSpawner objectSpawner;
	[Export] WinProgress winProgress;
	[Export] Node2D WinState;
	[Export] GameTimer gameTimer;
	public int score = 0;
	public int health = 0;
	public bool IsGameOver = false;
	public bool IsGameWon = false;
	bool walkingtoteacher = false;
	
	public override void _Ready()
	{
		GD.Print("Mananger Ready");
		health = player.baseHealth;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		scoreLabel.Text = "Score: " + score;
		healthLabel.Text = "Health: " + health;

		if(IsGameOver)
		{
			GameOver();
		}

		//Update Progress Bar
		winProgress.SetBarValue(score);

		if(winProgress.GameWon)
		{
			IsGameWon = true;
			WinState.Visible = true;
			objectSpawner.is_Spawning = false;
			if(player.Position.Y == player.startingPostion.Y)	//when player is on floor movement is locked.
			{
				player.CanMove = false;
			}

			//Stop 'Movement' of all hazards
			foreach(Node2D node in GetTree().Root.GetChildren())
			{
				if(node is BaseHazard)
				{
					BaseHazard hazard = (BaseHazard)node;
					if(hazard.originSpawnerIndex == 2)	// This means its hazard on floor.
					{
						hazard.Speed = 0;
					}
					if(hazard.originSpawnerIndex == 1 || hazard.originSpawnerIndex == 0)	// This means its hazard on ceiling.
					{
						hazard.Speed = -2;	
					}
				}	
			}
			// Walk player to teacher
			if(!walkingtoteacher)
			{
				player.WalkTeacherAnimation();
				walkingtoteacher = true;
			}

			// Show Win Screen
			winScreen.Visible = true;

			// Hide progress bar and health bar
			winProgress.Visible = false;
			healthLabel.Visible = false;

			// Update Win Screen
			gameTimer.stopTimer();
			winScreen.SetTime(gameTimer.GetTime());
			winScreen.SetLives(health);

			// Make player invincible
			player.invincible = true;
		}
		
	}

	private void _on_successful_avoidance_area_area_entered(Area2D area)
	{
		if(area.GetParent() is BaseHazard && !IsGameOver)
		{
			//GD.Print("Im hit");
			score++;
		}
	}
	public void UpdateHealth(int pHealth)
	{
		health = pHealth;
	}

	private void GameOver()
	{
		healthLabel.Text = "Health: 0";	//Make sure health is 0
		gameOverScreen.Visible = true;
		objectSpawner.is_Spawning = false;
		player.CanMove = false;

		gameOverScreen.SetScore(score);

		

	}
		
}


