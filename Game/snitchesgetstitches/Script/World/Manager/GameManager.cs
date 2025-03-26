using Godot;
using System;

public partial class GameManager : Node2D
{
	[Export] Label scoreLabel;
	public int score = 0;
	public override void _Ready()
	{
		GD.Print("Mananger Ready");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		scoreLabel.Text = "Score: " + score;
	}

	private void _on_successful_avoidance_area_area_entered(Area2D area)
	{
		GD.Print("Im hit");
		score++;
	}
}
