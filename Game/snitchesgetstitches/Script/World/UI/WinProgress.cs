using Godot;
using System;

public partial class WinProgress : Control
{
	[Export] public bool WinProgressHardMode = false;
	[Export] public int BarValue = 0;
	[Export] ProgressBar progressBar;
	public bool GameWon = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(WinProgressHardMode)
		{
			progressBar.MaxValue = 80;
			progressBar.MinValue = 0;
		}
		else
		{
			progressBar.MaxValue = 50;
			progressBar.MinValue = 0;
		}
		
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
			//GD.Print("You Win");
			GameWon = true;
		}
		
	}
	public void SetBarValue(int value)
	{
		BarValue = value;
	}
}
