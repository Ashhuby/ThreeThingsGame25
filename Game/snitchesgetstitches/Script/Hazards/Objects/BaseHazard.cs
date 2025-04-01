using Godot;
using System;

public partial class BaseHazard : Node2D
{
	[Export] public float Speed = -9;
	public int originSpawnerIndex = -1;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(Speed, 0);
	}
	private void _on_hazard_hit_box_body_entered(Node2D body)
	{
		
		
	}

	private void _on_hazard_hit_box_area_entered(Area2D area)
	{
		if(area.GetParent().GetParent() is Player)
		{
			
			//GD.Print("Player Hit");
			Player p = (Player)area.GetParent().GetParent();
			p.PlayerTakesDamage(1);
			if(!p.invincible)
			{
				QueueFree();
			}
		}

	}
}
