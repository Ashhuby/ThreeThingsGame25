using Godot;
using System;

public partial class ShakyCam : Camera2D
{
	private float shakeDuration = 0f;
	private float shakeMagnitude = 0f;
	private Vector2 originalPosition;
	
	public override void _Ready()
	{
		// Store the original position of the camera when the scene starts
		originalPosition = Position;
	}
	
	public override void _Process(double delta)
	{
		// If shake duration is greater than 0, continue shaking the camera
		if (shakeDuration > 0)
		{
			// Generate random offsets for the shake
			float offsetX = (float)(GD.Randf() * 2 - 1) * shakeMagnitude;
			float offsetY = (float)(GD.Randf() * 2 - 1) * shakeMagnitude;

			// Apply the offset to the camera's position
			Position = new Vector2(originalPosition.X + offsetX, originalPosition.Y + offsetY);

			// Decrease the shake duration
			shakeDuration -= (float)delta;
		}
		else
		{
			// Reset the camera's position after the shake ends
			Position = originalPosition;
		}
	}
	
	// Method to start the shake effect
	public void StartShake(float duration, float magnitude)
	{
		shakeDuration = duration;
		shakeMagnitude = magnitude;
	}
}
