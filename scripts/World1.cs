using Godot;

public partial class World1 : Node2D
{
	public NodePath playerPath = "Player";
	public NodePath CatPath = "AishaCat";
	private NodePath cameraPath = "camera";
	public Camera2D camera;
	public PackedScene catScene = (PackedScene)ResourceLoader.Load("res://actors/AishaCat.tscn");
	public PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://actors/Player.tscn");

	public override void _Ready()
	{
		this.camera = GetNode<Camera2D>(cameraPath);
		Player.position = Globals.playerPosition;
	}
	public override void _Process(double delta)
	{
		if (Player.isTransformedToCat && !AishaCat.isDeath)
		{
			AishaCat.FollowCamera(camera);
		} 
		if (!Player.isTransformedToCat && !Player.isDeath)
		{
		 	Player.FollowCamera(camera);
		}
	}
}
