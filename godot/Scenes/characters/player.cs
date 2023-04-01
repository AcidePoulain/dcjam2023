using Godot;

public partial class player : CharacterBody3D
{
    public const float Speed = 5.0f;
    public const float JumpVelocity = 4.5f;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= gravity * (float)delta;

        Velocity = velocity;
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("forward"))
        {
            var frontRayCast = GetNode<RayCast3D>("FrontCollisionRaycast");

            if (!frontRayCast.IsColliding()) {
                var currentPosition = this.Position;
                currentPosition.Z -= 2;
                this.Position = currentPosition;
            }

            return;
        }

        if (@event.IsActionPressed("backward"))
        {
           var backRayCast = GetNode<RayCast3D>("BackCollisionRaycast");

            if (!backRayCast.IsColliding()) {
                var currentPosition = this.Position;
                currentPosition.Z += 2;
                this.Position = currentPosition;
            }

            return;
        }

        if (@event.IsActionPressed("rotate_left"))
        {
            // rotate to the left
        }

        if (@event.IsActionPressed("rotate_right"))
        {
            // rotate to the right
        }
}
}
