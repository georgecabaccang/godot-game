using System;
using Godot;

public partial class PlayerMovement : AnimatedSprite2D
{
	String lastKeyPressed = "";
	String stopPosition = "";

	float movementAmountUpAndDown;
	float movementAmountRightAndLeft;
	float axisMovementAmount;

	int numberOfKeysPressedAtTheSameTime = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() { }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (numberOfKeysPressedAtTheSameTime >= 3)
		{
			movementAmountUpAndDown = 0;
			movementAmountRightAndLeft = 0;
			axisMovementAmount = 0;
			this.Stop();
		}

		// modifier for sprite animation and movement speed. (AKA sprint)
		if (!Input.IsKeyPressed(Key.Shift) && numberOfKeysPressedAtTheSameTime < 3)
		{
			movementAmountUpAndDown = 0.01f;
			movementAmountRightAndLeft = 0.02f;
			axisMovementAmount = 0.02f;
		}
		else if (Input.IsKeyPressed(Key.Shift) && numberOfKeysPressedAtTheSameTime < 3)
		{
			movementAmountUpAndDown = 0.03f;
			movementAmountRightAndLeft = 0.04f;
			axisMovementAmount = 0.05f;
			this.SpeedScale = 1.5f;
		}

		numberOfKeysPressedAtTheSameTime = 0;

		// handles stopping of movements _____________________
		bool isMoving =
			Input.IsKeyPressed(Key.W)
			|| Input.IsKeyPressed(Key.D)
			|| Input.IsKeyPressed(Key.A)
			|| Input.IsKeyPressed(Key.S);

		if (
			!isMoving
			|| (Input.IsKeyPressed(Key.D) && Input.IsKeyPressed(Key.A))
			|| (Input.IsKeyPressed(Key.W) && Input.IsKeyPressed(Key.S))
		)
		{
			this.Play();
			this.SetFrameAndProgress(1, 1);
		}
		// ___________________________________________________

		// *********************** direction per key pressed ************************
		// x and y axis movement
		if (
			Input.IsKeyPressed(Key.W)
			&& !Input.IsKeyPressed(Key.D)
			&& !Input.IsKeyPressed(Key.A)
			&& !Input.IsKeyPressed(Key.S)
		)
		{
			numberOfKeysPressedAtTheSameTime++;
			this.Play("north");
			this.Position += new Vector2(0, -axisMovementAmount);
			lastKeyPressed = "w";
		}
		if (
			Input.IsKeyPressed(Key.S)
			&& !Input.IsKeyPressed(Key.D)
			&& !Input.IsKeyPressed(Key.A)
			&& !Input.IsKeyPressed(Key.W)
		)
		{
			numberOfKeysPressedAtTheSameTime++;
			this.Play("south");
			this.Position += new Vector2(0, axisMovementAmount);
			lastKeyPressed = "s";
		}
		if (
			Input.IsKeyPressed(Key.A)
			&& !Input.IsKeyPressed(Key.S)
			&& !Input.IsKeyPressed(Key.W)
			&& !Input.IsKeyPressed(Key.D)
		)
		{
			numberOfKeysPressedAtTheSameTime++;
			this.Play("west");
			this.Position += new Vector2(-axisMovementAmount, 0);
			lastKeyPressed = "a";
		}
		if (
			Input.IsKeyPressed(Key.D)
			&& !Input.IsKeyPressed(Key.S)
			&& !Input.IsKeyPressed(Key.W)
			&& !Input.IsKeyPressed(Key.A)
		)
		{
			numberOfKeysPressedAtTheSameTime++;
			this.Play("east");
			this.Position += new Vector2(axisMovementAmount, 0);
			lastKeyPressed = "d";
		}

		// diagonal movement
		if (Input.IsKeyPressed(Key.W) && Input.IsKeyPressed(Key.D))
		{
			numberOfKeysPressedAtTheSameTime += 2;
			this.Play("north_east");
			this.Position += new Vector2(movementAmountRightAndLeft, -movementAmountUpAndDown);
			lastKeyPressed = "w+d";
		}
		if (Input.IsKeyPressed(Key.S) && Input.IsKeyPressed(Key.A))
		{
			numberOfKeysPressedAtTheSameTime += 2;
			this.Play("south_west");
			this.Position += new Vector2(-movementAmountRightAndLeft, movementAmountUpAndDown);
			lastKeyPressed = "s+a";
		}
		if (Input.IsKeyPressed(Key.W) && Input.IsKeyPressed(Key.A))
		{
			numberOfKeysPressedAtTheSameTime += 2;
			this.Play("north_west");
			this.Position += new Vector2(-movementAmountRightAndLeft, -movementAmountUpAndDown);
			lastKeyPressed = "w+a";
		}
		if (Input.IsKeyPressed(Key.S) && Input.IsKeyPressed(Key.D))
		{
			numberOfKeysPressedAtTheSameTime += 2;
			this.Play("south_east");
			this.Position += new Vector2(movementAmountRightAndLeft, movementAmountUpAndDown);
			lastKeyPressed = "s+d";
		}

		// **********************************************************************
	}
}
