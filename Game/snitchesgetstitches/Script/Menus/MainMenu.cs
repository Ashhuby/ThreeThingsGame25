using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] HowToPlayScreen HowToPlay;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/World/sGame.tscn");
	}
	public void _on_how_to_play_pressed()
	{
		HowToPlay.is_Active = true;
	}
	public void _on_exit_pressed()
	{
		GetTree().Quit();
	}
}
