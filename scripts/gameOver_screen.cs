using Godot;
using System;

public partial class gameOver_screen : Control
{

	private void _on_restart_btn_pressed(){

			GetTree().ChangeSceneToFile("res://scenes/gameOver_screen.tscn");
	}



	private void _on_quit_btn_pressed(){
		GetTree().Quit();
	}

}
