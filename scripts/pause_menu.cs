	using Godot;
	using System;

	public partial class pause_menu : CanvasLayer
	{

		public override void _Ready()
		{
			Visible = false;
		}

		public override void _Process(double delta)
		{
		}
		
		public override void _UnhandledInput(InputEvent @event)
		{
			if (@event.IsActionPressed("ui_cancel"))
			{
				Visible = true;
				GetTree().Paused = true;
			}
		}
		
		private void _on_resume_btb_pressed()
		{
			GetTree().Paused = false;
			Visible = false;
			
		}
		
		private void OnMenuBtnPressed()
		{
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile("res://prefabs/TitleScreen.tscn");
		}

		private void _on_quit_btn_pressed()
		{
			GetTree().Quit();
		}
	}



