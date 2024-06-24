using Godot;
using MySqlConnector;

public partial class UserSession : Node
{
    public string userSessionEmail;
    public string passwordUserSession;
    public static bool isLogin = false;
	public static MySqlConnection conn;
	public string connectionString = OS.GetEnvironment("MY_DB_CONNECTION_STRING");

    public override void _Ready()
    {
        conn = new MySqlConnection(connectionString);
    }
}