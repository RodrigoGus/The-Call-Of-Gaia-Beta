using Godot;
using MySqlConnector;

public partial class UserSession : Node
{
    public string userSessionEmail;
    public string passwordUserSession;
    public static bool isLogin = false;
	public static MySqlConnection conn;
	public string connectionString = "server=tcog_db.mysql.dbaas.com.br;user=tcog_db;database=tcog_db;password=tcogdb@T1";

    public override void _Ready()
    {
        conn = new MySqlConnection(connectionString);
    }
}