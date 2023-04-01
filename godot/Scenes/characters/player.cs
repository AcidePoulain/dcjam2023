using Godot;

public partial class player : CharacterBody3D
{
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
                this.Translate(new Vector3(0, 0, -1));
            }

            return;
        }

        if (@event.IsActionPressed("backward"))
        {
           var backRayCast = GetNode<RayCast3D>("BackCollisionRaycast");

            if (!backRayCast.IsColliding()) {
                this.Translate(new Vector3(0, 0, 1));
            }

            return;
        }

        if (@event.IsActionPressed("rotate_left"))
        {
            var tween = this.CreateTween();
            var rotation = this.RotationDegrees;
            rotation.Y += 90;
            tween.TweenProperty(this, "rotation_degrees", rotation, 1.0f);
        }

        if (@event.IsActionPressed("rotate_right"))
        {
            var tween = this.CreateTween();
            var rotation = this.RotationDegrees;
            rotation.Y -= 90;
            tween.TweenProperty(this, "rotation_degrees", rotation, 1.0f);
        }
}
}
