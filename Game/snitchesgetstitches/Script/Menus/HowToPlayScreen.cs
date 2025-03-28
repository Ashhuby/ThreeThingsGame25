using Godot;
using System;

public partial class HowToPlayScreen : Control
{
	[Export] public bool is_Active = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(is_Active)
		{
			this.Visible = true;
		}
		else
		{
			this.Visible = false;
		}
	}
}
