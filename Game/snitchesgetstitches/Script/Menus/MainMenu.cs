using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] HowToPlayScreen HowToPlay;
	public static bool HM = false;
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
		HM = false;
	}
	private void _on_play_hm_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/World/sGame.tscn");
		HM = true;
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
