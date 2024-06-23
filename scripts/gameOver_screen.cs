using Godot;
using System;

public partial class gameOver_screen : Control
{

	private void _on_restart_btn_pressed(){

		GetTree().ChangeSceneToFile("res://prefabs/TitleScreen.tscn");
	}



	private void _on_quit_btn_pressed(){
		GetTree().Quit();
	}

}
