using Godot;

public partial class world1 : Node2D
{
	public NodePath playerPath = "Player";
	public NodePath CatPath = "AishaCat";
	private NodePath cameraPath = "camera";
	public Camera2D camera;
	public PackedScene catScene = (PackedScene)ResourceLoader.Load("res://actors/AishaCat.tscn");
	public PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://actors/player.tscn");

	public override void _Ready()
	{
		this.camera = GetNode<Camera2D>(cameraPath);

		Checkpoint.load_game();
	}
	public override void _Process(double delta)
	{
		if (player.isTransformedToCat && !AishaCat.isDeath)
		{
			AishaCat.FollowCamera(camera);
		} 
		if (!player.isTransformedToCat && !player.isDeath)
		{
		 	player.FollowCamera(camera);
		}
	}
}
