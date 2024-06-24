using Godot;
using System;
using MySqlConnector;
public partial class signUp_menu : Control
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

	private void _on_sign_up_button_button_down()
	{
	   var email = _emailTextbox.Text;
	   var password = _passwordTextbox.Text;

	   try
	   {
			UserSession.conn.Open();
			string query = "INSERT INTO Users (email, user_password) VALUES (@email, @password)";
			using (var cmd = new MySqlCommand(query, UserSession.conn))
			{
				cmd.Parameters.AddWithValue("@email", email);
				cmd.Parameters.AddWithValue("@password", password);
				cmd.ExecuteNonQuery();
			}
			GD.Print("Usuário cadastrado com sucesso!");
			GetTree().ChangeSceneToFile("res://levels/LoginMenu.tscn");
	   }
	   catch (Exception e)
	   {
			GD.PrintErr("Erro ao conectar ao banco de dados: ", e.Message);
			GD.PrintErr("Stack Trace: ", e.StackTrace);
	   }
	   finally
	   {
	   }
	}
}
