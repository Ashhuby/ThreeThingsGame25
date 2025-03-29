using Godot;
using System;

public partial class WinProgress : Control
{
	[Export] public int BarValue = 0;
	[Export] ProgressBar progressBar;
	public bool GameWon = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		progressBar.Value = BarValue;

		if (BarValue > (int)progressBar.MaxValue)
		{
			BarValue = (int)progressBar.MaxValue;
		}
		if (BarValue < (int)progressBar.MinValue)
		{
			BarValue = (int)progressBar.MinValue;
		}
		if(BarValue == progressBar.MaxValue)
		{
			GD.Print("You Win");
			GameWon = true;
		}
		
	}
	public void SetBarValue(int value)
	{
		BarValue = value;
	}
}
