using Godot;
using System;

public partial class ObjectSpawner : Node2D
{

	[Export] public bool SpawnerHardMode = false;
	[Export] PackedScene Object;	//This is the hazard object
	[Export] PackedScene Collectable;	
	[Export] Node2D[] SpawnPoints;
	[Export] float eps = 1.5f;
	float spawn_Rate;
	float time_until_spawn = 0;
	public bool is_Spawning = true;

	public override void _Ready()
	{
		if(SpawnerHardMode)
		{
			eps = 2.0f;
		}
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
		int index = rng.RandiRange(0,SpawnPoints.Length-1);

		int hazardOrCollectable = rng.RandiRange(0,1);	//50% chance to spawn a hazard or collectable.

		PackedScene objectToSpawn = hazardOrCollectable == 0 ? Object : Collectable;

		Vector2 location = SpawnPoints[index].GlobalPosition;
		if(hazardOrCollectable == 0)
		{
			BaseHazard flyingObject = (BaseHazard)objectToSpawn.Instantiate();
			flyingObject.GlobalPosition = location;
			flyingObject.originSpawnerIndex = index;	//Set the index of the spawner that spawned this object.
			GetTree().Root.AddChild(flyingObject);
			if(SpawnerHardMode)
			{
				flyingObject.Speed = -12;
			}
			else
			{
				flyingObject.Speed = -10;
			}
			flyingObject.AddToGroup("Hazards");
		}
		else
		{
			BaseCollecable flyingObject = (BaseCollecable)objectToSpawn.Instantiate();
			flyingObject.GlobalPosition = location;
			flyingObject.originSpawnerIndex = index;	//Set the index of the spawner that spawned this object.
			GetTree().Root.AddChild(flyingObject);
			if(SpawnerHardMode)
			{
				flyingObject.Speed = -12;
			}
			else
			{
				flyingObject.Speed = -10;
			}
		}

		
	 }
}
