using Godot;

public partial class world1 : Node2D
{
	private NodePath playerPath = "Player";
	public player player;
	private NodePath cameraPath = "camera";
	public Camera2D camera;

	public override void _Ready()
	{
		this.player = GetNode<player>(playerPath);
		this.camera = GetNode<Camera2D>(cameraPath);
		checkpoint.load_game();
		player.FollowCamera(camera);
	}
	public override void _Process(double delta)
	{
	}
}
