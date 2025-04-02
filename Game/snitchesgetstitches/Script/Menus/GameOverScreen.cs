using Godot;
using System;
using System.Reflection.Metadata;

public partial class GameOverScreen : Control
{
	[Export] Label Score;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void SetScore(int score)
	{
		Score.Text = "Score: " + score;
	}
	private void _on_replay_pressed()
	{
		foreach (Node2D n in GetTree().GetNodesInGroup("Hazards"))
		{
			n.QueueFree();
		}
		GetTree().ChangeSceneToFile("res://Scene/World/sGame.tscn");
	}
	private void _on_exit_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/Menus/sMainMenu.tscn");
	}
}
