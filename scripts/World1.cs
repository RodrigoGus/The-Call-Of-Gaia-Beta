using Godot;

public partial class World1 : Node2D
{

	private NodePath cameraPath = "camera";
	public Camera2D camera;

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
		if(Player.isDeath || AishaCat.isDeath){
			Game_over();
		}
	}

	public void Game_over(){
		GetTree().ChangeSceneToFile("res://scenes/gameOver_screen.tscn");
	}
}
