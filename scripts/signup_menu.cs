using Godot;
using MySql.Data.MySqlClient;
using System;
using System.Data;

public partial class signup_menu : Control
{
	private MySqlConnection connection;
	private string sql = "INSERT INTO users (nickname, email, password) VALUES (@nickname, @email, @password)";

	private TextEdit _nicknameTextbox;
	private TextEdit _emailTextbox;
	private TextEdit _passwordTextbox;

	public override void _Ready()
	{
		_nicknameTextbox = GetNode<TextEdit>("nickname_textbox");
		_emailTextbox = GetNode<TextEdit>("email_textbox");
		_passwordTextbox = GetNode<TextEdit>("password_textbox");        

		TryOpenConnection();
	}

	private void TryOpenConnection()
	{
		try
		{
			string connectionString = "server=tcog_db.mysql.dbaas.com.br;uid=tcog_db;pwd=tcogdb@T1;database=tcog_db";
			connection = new MySqlConnection(connectionString);
			connection.Open();
			GD.Print("Connected to MySQL database.");
		}
		catch (Exception ex)
		{
			GD.Print("Error: " + ex.Message);
			if (ex.InnerException != null)
			{
				GD.Print("Inner Exception: " + ex.InnerException.Message);
			}
		}
	}

	private void _on_SignUpButton_button_down()
	{
		
		InsertUser(_nicknameTextbox.Text, _emailTextbox.Text, _passwordTextbox.Text);
	}

	private void InsertUser(string nickname, string email, string password)
	{
		if (connection.State != ConnectionState.Open)
		{
			TryOpenConnection();
		}

		using (var cmd = new MySqlCommand(sql, connection))
		{
			cmd.Parameters.AddWithValue("@nickname", nickname);
			cmd.Parameters.AddWithValue("@email", email);
			cmd.Parameters.AddWithValue("@password", password);
			try
			{
				cmd.ExecuteNonQuery();
				GD.Print("User inserted successfully.");
			}
			catch (Exception ex)
			{
				GD.Print("Error inserting user: " + ex.Message);
			}
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
