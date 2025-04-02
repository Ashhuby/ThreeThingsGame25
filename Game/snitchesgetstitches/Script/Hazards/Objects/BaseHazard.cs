using Godot;
using System;
using System.Linq;

public partial class BaseHazard : Node2D
{
	[Export] public float Speed = -9;
	public int originSpawnerIndex = -1;
	const float TargetFrameRate = 60f;
	[Export] public Node2D[] TopMidHazardSprites;
	[Export] public Node2D[] BotHazardSprites;

	public override void _Ready()
	{


		AddToGroup("Hazards");
		RandomNumberGenerator rng = new RandomNumberGenerator();
		//GD.Print(BotHazardSprites.Length);
		//GD.Print(TopMidHazardSprites.Length);

		//Bot
		if(originSpawnerIndex == 2)
		{
			int BotSprite = rng.RandiRange(0,BotHazardSprites.Length-1);
			for(int i = 0; i < BotHazardSprites.Length;i++)
			{
				if(i == BotSprite)
				{
					BotHazardSprites[i].Visible = true;
					//GD.Print(BotHazardSprites[i].Name);				
				}
				else
				{
					BotHazardSprites[i].Visible = false;
				}
			}

		}
		else //TopMid
		{
			int chosenSpriteIndex = rng.RandiRange(0,TopMidHazardSprites.Length-1);
			for(int i = 0; i < TopMidHazardSprites.Length;i++)
			{
				if(i == chosenSpriteIndex)
				{
					TopMidHazardSprites[i].Visible = true;
					//GD.Print(TopMidHazardSprites[1].Name);
				}
				else
				{
					TopMidHazardSprites[i].Visible = false;
				}
			}
		}
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(Speed * (float)delta * TargetFrameRate, 0);
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
