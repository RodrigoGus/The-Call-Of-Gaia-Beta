using Godot;
using System;

public partial class title_screen : Control
{
	[Export] public NodePath debugSaveNotFoundPath;
	public override void _Ready()
	{
	}
	
	private void _on_new_game_btn_pressed(){
		Checkpoint.load_game();
		GetTree().ChangeSceneToFile("res://levels/world1.tscn");

	}

	private void OnCarregaBtnPressed()
	{
		if (FileAccess.FileExists("user://savegame.txt"))
		{
			Checkpoint.load_game();
			GetTree().ChangeSceneToFile("res://levels/world1.tscn");
		} else {
			GetNode<Label>(debugSaveNotFoundPath).Visible = true;
		}
	}

	private void _on_quit_btn_pressed(){
		GetTree().Quit();
		
	}

	public override void _Process(double delta)
	{
	}
	
}
