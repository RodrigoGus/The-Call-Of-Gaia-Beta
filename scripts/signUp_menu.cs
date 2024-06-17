using Godot;
using System;
using MySqlConnector;

public partial class signUp_menu : Control
{
   private TextEdit _emailTextbox;
   private TextEdit _passwordTextbox;
   public MySqlConnection conn;
   public string conectionString = "server=tcog_db.mysql.dbaas.com.br;user=tcog_db;database=tcog_db;password=tcogdb@T1";

   public override void _Ready()
	{
	   _emailTextbox = GetNode<TextEdit>("ColorRect/email_box");
	   _passwordTextbox = GetNode<TextEdit>("ColorRect/password_box");
	   conn = new MySqlConnection(conectionString);
		
	}

	public override void _Process(double delta)
	{
	}

	private void _on_sign_up_button_button_down()
	{
	   conn.Open();
	   var email = _emailTextbox.Text;
	   var password = _passwordTextbox.Text;

	   try
	   {
		   string query = "INSERT INTO Users (email, user_password) VALUES (@email, @password)";
		   using (var cmd = new MySqlCommand(query, conn))
		   {
			   cmd.Parameters.AddWithValue("@email", email);
			   cmd.Parameters.AddWithValue("@password", password);
			   cmd.ExecuteNonQuery();
		   }
		   GD.Print("Usuário cadastrado com sucesso!");
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
