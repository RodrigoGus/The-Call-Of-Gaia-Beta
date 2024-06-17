using Godot;
using MySqlConnector;
using System;

public partial class LoginMenu : Control
{
	private TextEdit _emailTextbox;
	private TextEdit _passwordTextbox;
	public MySqlConnection conn;
	public string connectionString = "server=tcog_db.mysql.dbaas.com.br;user=tcog_db;database=tcog_db;password=tcogdb@T1";

	public override void _Ready()
	{
		_emailTextbox = GetNode<TextEdit>("ColorRect/email_box");
		_passwordTextbox = GetNode<TextEdit>("ColorRect/password_box");
		conn = new MySqlConnection(connectionString);
	}

	public override void _Process(double delta)
	{
	}

	private void OnSignInButtonPressed()
	{
		conn.Open();
		var email = _emailTextbox.Text;
		var password = _passwordTextbox.Text;

		try
		{
			string query = "SELECT email FROM Users WHERE email = @Email AND user_password = @Password";
			using (var cmd = new MySqlCommand(query, conn))
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
			GD.PrintErr("Erro ao conectar ao banco de dados: ", e.Message);
			GD.PrintErr("Stack Trace: ", e.StackTrace);
		}
		finally
		{
			conn.Close();
		}
	}
}



