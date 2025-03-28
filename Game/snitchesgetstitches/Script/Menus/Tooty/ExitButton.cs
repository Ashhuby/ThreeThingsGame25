using Godot;
using System;

public partial class ExitButton : Node2D
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
	private void _on_button_pressed()
	{
		GD.Print("Exit Button Pressed");
		//Hide the HowToPlayScreen
		HowToPlay.is_Active = false;
		
	}
}
