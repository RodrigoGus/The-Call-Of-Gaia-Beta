using Godot;
using System;

public partial class title_screen : Control
{

	public override void _Ready()
	{
	}
	
	private void _on_new_game_btn_pressed(){
		GetTree().ChangeSceneToFile("res://levels/world1.tscn");
	}


	private void _on_quit_btn_pressed(){
		GetTree().Quit();
		
	}


	public override void _Process(double delta)
	{
	}
}
