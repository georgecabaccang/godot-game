using Godot;

public partial class Cursor : AnimatedSprite3D {

    private InputController inputController;

    public override void _EnterTree() {
        inputController = (InputController)GetNode("../../../TestScene");

        inputController.Move += Move;
        inputController.Fire += Fire;
    }

    public void Move(Vector2 direction) {
        GD.Print(direction);
    }

    public void Fire(int action) {
        if (action == 1) {
            GD.Print("Confirm");
        }
        if (action == 0) {
            GD.Print("Cancel");
        }
    }
}
