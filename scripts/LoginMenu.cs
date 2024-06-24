using Godot;
using MySqlConnector;
using System;

public partial class LoginMenu : Control
{
	private TextEdit _emailTextbox;
	private TextEdit _passwordTextbox;

	public override void _Ready()
	{
		_emailTextbox = GetNode<TextEdit>("ColorRect/email_box");
		_passwordTextbox = GetNode<TextEdit>("ColorRect/password_box");
	}

	public override void _Process(double delta)
	{
	}
	private void OnSignUpButtonButtonDown()
	{
		GetTree().ChangeSceneToFile("res://levels/signUp_menu.tscn");
	}
	
	private void OnBackBtnPressed()
	{
		GetTree().ChangeSceneToFile("res://prefabs/TitleScreen.tscn");
	}

	private void OnSignInButtonPressed()
	{
		UserSession.conn.Open();
		var email = _emailTextbox.Text;
		var password = _passwordTextbox.Text;

		try
		{
			string query = "SELECT email FROM Users WHERE email = @Email AND user_password = @Password";
			using (var cmd = new MySqlCommand(query, UserSession.conn))
			{
				cmd.Parameters.AddWithValue("@Email", email);
				cmd.Parameters.AddWithValue("@Password", password);

				using (var reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						string userEmail = reader.GetString("email");


						var userSession = (UserSession)GetNode("/root/UserSession");
						userSession.userSessionEmail = userEmail;

						GD.Print("Login bem-sucedido! Bem-vindo");
						UserSession.isLogin = true;
						GetTree().ChangeSceneToFile("res://prefabs/TitleScreen.tscn");
					}
					else
					{
						GD.Print("Email ou senha incorretos!");
					}
				}
			}
		}
		catch (Exception e)
		{
			UserSession.conn.Close();
			GD.PrintErr("Erro ao conectar ao banco de dados: ", e.Message);
			GD.PrintErr("Stack Trace: ", e.StackTrace);
		}
		finally
		{
		}
	}
}









