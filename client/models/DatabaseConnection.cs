using Microsoft.Data.SqlClient;
using System;
using System.Windows;
public sealed class DatabaseConnection
{
	private static readonly DatabaseConnection instance = new DatabaseConnection();
	private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
	
	private SqlConnection connection;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static DatabaseConnection()
	{
	}

	private DatabaseConnection()
	{
		builder.ConnectionString = $"Data Source=DESKTOP-69V3Q5U\\SQLEXPRESS;Initial Catalog = blank-feed;TrustServerCertificate=True;MultipleActiveResultSets=True; Trusted_Connection=True";
		// Initialize your database connection here
		connection = new SqlConnection(builder.ConnectionString);
		
	}

	public static DatabaseConnection Instance
	{
		get
		{
			return instance;
		}
	}

	public SqlConnection GetConnection()
	{
		
		if (connection == null)
		{
			connection = new SqlConnection(builder.ConnectionString);
		}
		

		return connection;
	}
}