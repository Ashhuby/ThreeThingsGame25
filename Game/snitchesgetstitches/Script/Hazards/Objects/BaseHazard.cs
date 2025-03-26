using Godot;
using System;

public partial class BaseHazard : Node2D
{
	[Export] float Speed = -4;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(Speed, 0);
	}
}
