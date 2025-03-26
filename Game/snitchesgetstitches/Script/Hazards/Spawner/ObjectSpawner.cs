using Godot;
using System;

public partial class ObjectSpawner : Node2D
{
	[Export] PackedScene Object;
	[Export] Node2D[] SpawnPoints;
	[Export] float eps = 0.5f;
	float spawn_Rate;
	float time_until_spawn = 0;
	public bool is_Spawning = true;

	public override void _Ready()
	{
		spawn_Rate = 1 / eps;
	}

	public override void _Process(double delta)
	{
		if(is_Spawning == true)
		{
			if (time_until_spawn > spawn_Rate) 
			{
				Spawn();
				time_until_spawn = 0;
			}
			else
			{
				time_until_spawn += (float)delta;
			}
		}
	}

	 private void Spawn()
	 {
		
		RandomNumberGenerator rng = new RandomNumberGenerator();
		Vector2 location = SpawnPoints[rng.RandiRange(0,SpawnPoints.Length-1)].GlobalPosition;
		BaseHazard flyingObject = (BaseHazard)Object.Instantiate();
		flyingObject.GlobalPosition = location;
		GetTree().Root.AddChild(flyingObject);
	 }
}
