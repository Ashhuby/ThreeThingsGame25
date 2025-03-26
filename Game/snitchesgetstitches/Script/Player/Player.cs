using Godot;
using System;

public partial class Player : Node2D
{
	[Export] Sprite2D RunningSprite;
	[Export] Sprite2D CrounchingSprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RunningSprite.Visible = true;
		CrounchingSprite.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("Crouch"))
		{
			RunningSprite.Visible = false;
			CrounchingSprite.Visible = true;
		}
		if(Input.IsActionJustReleased("Crouch"))
		{
			RunningSprite.Visible = true;
			CrounchingSprite.Visible = false;
		}

		if(Input.IsActionJustPressed("Jump"))
		{
			Jump((float)delta);
		}
	}

	private void Jump(float delta)
	{
		GD.Print("Jumping");
		Vector2 velocity = new Vector2(0, -5000);
		Position += velocity  * (float)delta;
	}
	
}
