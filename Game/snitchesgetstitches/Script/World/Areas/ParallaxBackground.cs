using Godot;

public partial class ParallaxBackground : Godot.ParallaxBackground
{
	[Export] public float ScrollSpeed = 450f;	//200 //350

	public override void _Process(double delta) // Change float to double
	{
		ScrollOffset -= new Vector2(ScrollSpeed * (float)delta, 0);
	}
	public void GameWin()
	{	
		ScrollSpeed = 0f;
	}
}
