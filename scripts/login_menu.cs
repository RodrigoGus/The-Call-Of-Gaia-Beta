using Godot;
// using Npgsql;
using System;
using MySql.Data.MySqlClient;
using System.Data;
public partial class login_menu : Control
{

	MySql.Data.MySqlClient.MySqlConnection connection;
	string connectionString = "Server=tcog_db.mysql.dbaas.com.br;Uid=tcog_db;Pwd=tcogdb@T1;Database=tcog_db";



	string user_nickname = "";
	string user_email = "";
	string user_password = "";
	TextEdit _nickname_textbox;
	TextEdit _email_textbox;
	TextEdit _password_textbox;

	public override void _Ready()
	{

		_nickname_textbox = GetNode<TextEdit>("nickname_textbox");
		_email_textbox = GetNode<TextEdit>("email_textbox");
		_password_textbox = GetNode<TextEdit>("password_textbox");		

		try
		{
			connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
			connection.Open();
			GD.Print("Connected to MySQL database.");
		}
		catch (Exception ex)
		{
			GD.Print("teste Error: " + ex.Message);
			if (ex.InnerException != null)
			{
				GD.Print("Inner teste Exception: " + ex.InnerException.Message);
			}
		}

	}
	public override void _Process(double delta)
	{

	}
	private void _on_sign_up_button_button_down()
	{

		user_nickname = _nickname_textbox.Text;
		user_email = _email_textbox.Text;
		user_password = _password_textbox.Text.Sha256Text();
		InsertUser();
		
	}
	private void InsertUser()
	{
		if (connection.State != ConnectionState.Open)
		{
			try
			{
				connection.Open();
			}
			catch (Exception ex)
			{
				GD.Print("Error opening connection: " + ex.Message);
				return;
			}
		}

		try
		{
			string sql = "INSERT INTO users (nickname, email, password) VALUES (@nickname, @email, @password)";
			MySqlCommand cmd = new MySqlCommand(sql, connection);
			cmd.Parameters.AddWithValue("@nickname", user_nickname);
			cmd.Parameters.AddWithValue("@email", user_email);
			cmd.Parameters.AddWithValue("@password", user_password);
			cmd.ExecuteNonQuery();
			GD.Print("User inserted successfully.");
		}
		catch (Exception ex)
		{
			GD.Print("Error inserting user: " + ex.Message);
		}
		
	}

	public override void _ExitTree()
	{
		if (connection != null && connection.State == ConnectionState.Open)
		{
			connection.Close();
			GD.Print("Connection closed.");
		}
	}

}
