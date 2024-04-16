using Godot;
using Npgsql;
using System;
public partial class login_menu : Control
{
	static string Server = "kesavan.db.elephantsql.com";
	static string User = "ljvrzmkj";
	static string DB = "ljvrzmkj";
	static string Password = "JuMSnVxR3iln2-VB4pQav1tgd9lH4MTZ";
	static string connectionString = "Host=kesavan.db.elephantsql.com;Username=ljvrzmkj;Password=JuMSnVxR3iln2-VB4pQav1tgd9lH4MTZ;Database=ljvrzmkj";
	NpgsqlConnection Connection = new NpgsqlConnection(connectionString);
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
		Connection.Open();
		var cmd = new NpgsqlCommand("INSERT INTO users (nickname, email, password) VALUES (@nickname, @email, @password)", Connection);
		
			cmd.Parameters.AddWithValue("nickname", user_nickname);
			cmd.Parameters.AddWithValue("email", user_email);
			cmd.Parameters.AddWithValue("password", user_password);
			cmd.ExecuteNonQuery();
		
		
	}

}
	


