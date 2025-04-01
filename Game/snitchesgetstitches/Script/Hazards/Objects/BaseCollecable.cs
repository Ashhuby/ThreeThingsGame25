using Godot;
using System;
using System.IO;

public partial class BaseCollecable : Node2D
{
	
	[Export] public float Speed = -9;
	public int originSpawnerIndex = -1;
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(Speed, 0);
	}

	private void _on_collectable_hit_box_area_entered(Area2D area)
	{
		if(area.GetParent().GetParent() is Player)
		{
			Player p = (Player)area.GetParent().GetParent();
			try
			{
				Node2D path;
				try
				{
					path = p?.GetParent()?.GetParent()?.GetNodeOrNull<Node2D>("GameManager");
				}
				catch{path = null;}
				
				if(path is GameManager)
				{
					GameManager gm = (GameManager)path;
              	    if (gm != null) gm.score += 3;  // Adds 3 to score.
				}
				
            }
			catch
			{
				GD.Print("No GM");
			}
			try
			{
				EndlessGameManager egm = p.GetParent().GetParent().GetNode<EndlessGameManager>("EndlessGameManager");
				if(egm != null) egm.Endlessscore += 3;	// Adds 3 to score.
			}
			catch{}
			
			if(!p.invincible)	
			{
				QueueFree();
			}
			
		}

	}

}
