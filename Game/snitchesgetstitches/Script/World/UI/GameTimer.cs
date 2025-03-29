using Godot;
using System;

public partial class GameTimer : Node2D
{
	double time = 0;
	bool counting = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		counting = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(counting)
		{
			time += delta;
			GD.Print("Time: " + time);
		}
	}
	public void stopTimer()
	{
		counting = false;
	}

	public double GetTime()
	{
		return time;
	}
}
