using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class WinScreen : Control
{
	public double timeTaken = 0;
	public int lives = 0;
	[Export] Label timeTakenLabel;
	[Export] Label livesLeft;
	public static bool WinScreenHM = false;

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		
		timeTakenLabel.Text =  Math.Round(timeTaken, 2) + "s";	//Round to 3 decimal places
		livesLeft.Text =  $"{lives}";
		
		MainMenu.HM = false;	//Reset HardMode to false
	}

	public void SetTime(double time)
	{
		timeTaken = time;
	}
	public void SetLives(int pLives)
	{
		lives = pLives;
	}

	private void _on_replay_nm_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/World/sGame.tscn");
		WinScreenHM = false;
	}

	private void _on_replay_hm_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/World/sGame.tscn");
		WinScreenHM = true;
	}

	private void _on_exit_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/Menus/sMainMenu.tscn");
	}
}
