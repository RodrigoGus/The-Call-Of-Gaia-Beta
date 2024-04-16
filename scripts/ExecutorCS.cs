// using Godot;
// using System.Data;
// using Npgsql;
// using System;


// public partial class ExecutorCS : Node
// {
// 	private bool isConnectionOpen = false;
// 	static string Server = "kesavan.db.elephantsql.com";
// 	static string User = "ljvrzmkj";
// 	static string DB = "ljvrzmkj";
// 	static string Password = "JuMSnVxR3iln2-VB4pQav1tgd9lH4MTZ";
// 	NpgsqlConnection Connection = new NpgsqlConnection(Server + ";" + User + ";" + Password + ";" + DB);

// 	public void Start()
// 	{
// 		Connection.Open();
// 		isConnectionOpen = true;
// 	}
// 	public override void _Process(double delta)
// 	{
// 	}

// 	public void UserRegister(string nickname, string email, string password)
// 	{
// 		int tableSize;
// 		string scriptTableSize = "SELECT COUNT(user_id) FROM Users";
// 		NpgsqlCommand commandTableSize = new NpgsqlCommand(scriptTableSize, Connection);
// 		tableSize = Convert.ToInt32(commandTableSize.ExecuteScalar());

// 	}
// }
