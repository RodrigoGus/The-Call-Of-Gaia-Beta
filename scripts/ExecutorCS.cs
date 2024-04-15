using Godot;
using System.Data;
using Npgsql;


public partial class ExecutorCS : Node
{
	private bool connectionOpen = false;
	static string Server = "kesavan.db.elephantsql.com";
	static string User = "ljvrzmkj";
	static string DB = "ljvrzmkj";
	static string Password = "JuMSnVxR3iln2-VB4pQav1tgd9lH4MTZ";
	NpgsqlConnection Connection = new NpgsqlConnection(Server + ";" + User + ";" + Password + ";" + DB);

	public void Start()
	{
		Connection.Open();
		connectionOpen = true;
	}
	public override void _Process(double delta)
	{
	}
}
