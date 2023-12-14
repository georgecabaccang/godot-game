using System;
using Godot;

public partial class PlayerMovement : AnimatedSprite2D
{
	String lastKeyPressed = "";
	String stopPosition = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() { }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float movementAmountUpAndDown = 0.01f;
		float movementAmountRightAndLeft = 0.02f;

		if (Input.IsKeyPressed(Key.Shift))
		{
			movementAmountUpAndDown = 0.03f;
			movementAmountRightAndLeft = 0.04f;
			this.SpeedScale = 1.5f;
		}

		if (!Input.IsAnythingPressed())
		{
			switch (lastKeyPressed)
			{
				case "w":
					stopPosition = "idle_north_east";
					break;
				case "s":
					stopPosition = "idle_south_west";
					break;
				case "a":
					stopPosition = "idle_north_west";
					break;
				case "d":
					stopPosition = "idle_south_east";
					break;
			}
			this.Play(stopPosition);
			this.Stop();
		}

		if (Input.IsKeyPressed(Key.W) && !Input.IsKeyPressed(Key.D) && !Input.IsKeyPressed(Key.A))
		{
			this.Play("north_east");
			this.Position += new Vector2(movementAmountRightAndLeft, -movementAmountUpAndDown);
			lastKeyPressed = "w";
		}
		if (Input.IsKeyPressed(Key.S) && !Input.IsKeyPressed(Key.D) && !Input.IsKeyPressed(Key.A))
		{
			this.Play("south_west");
			this.Position += new Vector2(-movementAmountRightAndLeft, movementAmountUpAndDown);
			lastKeyPressed = "s";
		}
		if (Input.IsKeyPressed(Key.A))
		{
			this.Play("north_west");
			this.Position += new Vector2(-movementAmountRightAndLeft, -movementAmountUpAndDown);
			lastKeyPressed = "a";
		}
		if (Input.IsKeyPressed(Key.D))
		{
			this.Play("south_east");
			this.Position += new Vector2(movementAmountRightAndLeft, movementAmountUpAndDown);
			lastKeyPressed = "d";
		}
	}
}
