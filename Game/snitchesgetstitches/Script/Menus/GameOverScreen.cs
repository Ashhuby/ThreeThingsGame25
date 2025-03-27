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
}
