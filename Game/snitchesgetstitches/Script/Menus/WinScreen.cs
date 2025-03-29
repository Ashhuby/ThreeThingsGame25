using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class WinScreen : Control
{
	public double timeTaken = 0;
	public int lives = 0;
	[Export] Label timeTakenLabel;
	[Export] Label livesLeft;

    public override void _Ready()
    {
        
    }

	public override void _Process(double delta)
	{
		
		timeTakenLabel.Text = "Snitch Time: \n" + Math.Round(timeTaken, 3) + "s";	//Round to 3 decimal places
		livesLeft.Text = "Lives Left: \n" + lives;
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
	}

	private void _on_replay_hm_pressed()
	{
		GD.Print("Hard Mode");
	}

	private void _on_exit_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/Menus/sMainMenu.tscn");
	}
}
