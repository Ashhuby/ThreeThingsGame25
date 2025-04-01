using Godot;
using GodotPlugins.Game;
using System;

public partial class EndlessGameManager : Node2D
{
	[Export] public bool EndLessGameManagerHardMode = false;
	[Export] Label EndlessscoreLabel;
	[Export] Label EndlesshealthLabel;
	[Export] Player Endlessplayer; 
	[Export] GameOverScreen EndlessgameOverScreen;
	[Export] WinScreen EndlesswinScreen;
	[Export] ObjectSpawner EndlessobjectSpawner;
	[Export] GameTimer EndlessgameTimer;
	public int Endlessscore = 0;
	public int Endlesshealth = 0;
	public bool EndlessIsGameOver = false;
	public bool EndlessIsGameWon = false;
	

	public override void _Ready()
	{
	
		Endlesshealth = Endlessplayer.baseHealth; 	
		if(EndLessGameManagerHardMode)
		{
			Endlessplayer.PlayerHardMode = true;
			EndlessobjectSpawner.SpawnerHardMode = true;
			GD.Print("HardMode");
		}
		else 
		{
			GD.Print("NormalMode");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		EndlessscoreLabel.Text = "Score: " + Endlessscore;
		EndlesshealthLabel.Text = "Health: " + Endlesshealth;

		if(EndlessIsGameOver)
		{
			EndlessGameOver();
		}		
		
	}

	private void endlessOnEtner(Area2D area)
	{
		if(area.GetParent() is BaseHazard && !EndlessIsGameOver)
		{
			//GD.Print("Im hit");
			Endlessscore++;
		}
	}
	public void EndlessUpdateHealth(int pHealth)
	{
		Endlesshealth = pHealth;
	}

	private void EndlessGameOver()
	{
		EndlesshealthLabel.Text = "Health: 0";	//Make sure health is 0
		EndlessgameOverScreen.Visible = true;
		EndlessobjectSpawner.is_Spawning = false;
		Endlessplayer.CanMove = false;
		EndlessscoreLabel.Visible = false; //Hide score
		

		EndlessgameOverScreen.SetScore(Endlessscore);

		MainMenu.HM = false;	//Reset HardMode to false
		WinScreen.WinScreenHM = false;	//Reset HardMode to false

		

	}
		
}


