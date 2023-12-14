using System;
using Godot;

public partial class character_body : CharacterBody2D
{
	public const float Speed = 300.0f;

	// public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	// public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// // Add the gravity.
		// if (!IsOnFloor())
		// 	velocity.Y += gravity * (float)delta;

		// // Handle Jump.
		// if (Input.IsActionJustPressed("jump") && IsOnFloor())
		// 	velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		// GD.Print(Vector2.Up.Y);
		// GD.Print(direction.Y);

		// if (direction.Y == Vector2.Up.Y)
		// {
		// 	velocity.Y = direction.Y * Speed;
		// }
		// else if (direction.Y == Vector2.Down.Y)
		// {
		// 	velocity.Y = direction.Y * Speed;
		// }
		// if (direction.X == Vector2.Left.X)
		// {
		// 	velocity.X = direction.X * Speed;
		// }
		// else if (direction.X == Vector2.Right.X)
		// {
		// 	velocity.X = direction.X * Speed;
		// }

		// if (direction.X == 0 && direction.Y == 0)
		// {
		// 	velocity.Y = 0;
		// 	velocity.X = 0;
		// }
		if (direction != Vector2.Zero)
		{
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
