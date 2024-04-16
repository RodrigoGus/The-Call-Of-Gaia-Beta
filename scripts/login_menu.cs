using Godot;
using System;

public partial class login_menu : Control
{
	string nickname = "";
	string email = "";
	string password = "";
	bool created = false;
	TextEdit _nickname_textbox;

	public override void _Ready()
	{
		_nickname_textbox = GetNode<TextEdit>("nickname_textbox");
	}

	public override void _Process(double delta)
	{
	}

	private void _on_sign_up_button_button_down()
	{
		if (created){
			nickname = _nickname_textbox.Text;
		}
	}
}



