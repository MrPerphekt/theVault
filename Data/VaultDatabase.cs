using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

namespace theVault
{
	public class VaultDatabase
	{
		private readonly string DbName = "theVault.db3";
		
		private SqliteConnection _connection;

		public VaultDatabase ()
		{
			OpenConnection();	
		}
	
		~VaultDatabase()
		{
			CloseConnection();
		}
		
		private void CloseConnection()
		{
			if ( _connection != null )
				_connection.Close();
		}
		
		public List<Credential> GetCredentials()
		{
			List<Credential> credentials = new List<Credential>();
			
			if ( _connection == null )
				return credentials;
			
			try
			{
				using (var command = _connection.CreateCommand())
				{
					command.CommandType = CommandType.Text;
					command.CommandText = "SELECT * From Credentials";
					
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Credential credential = new Credential();
							credential.Id = reader.GetInt32(reader.GetOrdinal(CredentialFields.Id));
							credential.Name = reader.GetValue(reader.GetOrdinal(CredentialFields.Name)) as string;
							credential.Notes = reader.GetValue(reader.GetOrdinal(CredentialFields.Notes)) as string;
							credential.Password = reader.GetValue(reader.GetOrdinal(CredentialFields.Password)) as string;
							credential.Username = reader.GetValue(reader.GetOrdinal(CredentialFields.Username)) as string;
							credential.Url = reader.GetValue(reader.GetOrdinal(CredentialFields.Url)) as string;
							
							credentials.Add(credential);
						}
					}				
				}			
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}

			return credentials;
		}
		
		public bool SaveCredential(Credential credential)
		{
			if ( _connection == null )
				return false;
			
			try
			{
				using (var command = _connection.CreateCommand())
				{
					command.CommandType = CommandType.Text;
					command.CommandText = string.Format("INSERT INTO Credentials (Name, Notes, Password, Username, Url) VALUES ('{0}','{1}','{2}','{3}','{4}')", 
				                                    	credential.Name, credential.Notes, credential.Password, credential.Username, credential.Url);
					var id = command.ExecuteScalar();
					
					credential.Id = (int)id;
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				
				return false;
			}
			
			return true;
		}
		
		private void InitializeDatabase()
		{			
			var commands = new[] 
			{
            	"CREATE TABLE Credentials (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name ntext NOT NULL, Notes ntext, Password ntext, Username ntext, Url ntext)",
				"INSERT INTO Credentials (Name, Url) VALUES ('Amazon', 'http://www.amazon.com')",
				"INSERT INTO Credentials (Name, Url) VALUES ('Capital One', 'http://www.capitalone.com')",
				"INSERT INTO Credentials (Name, Url) VALUES ('Chase', 'http://www.chase.com')"
            };
			
            foreach (var cmd in commands)
			{
				try
				{
	                using (var command = _connection.CreateCommand()) 
					{
	                    command.CommandText = cmd;
	                    command.CommandType = CommandType.Text;
	                    _connection.Open();
	                    command.ExecuteNonQuery();
	                    _connection.Close();
	                }
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.Message);
				}			
			}
		}
			
		private void OpenConnection()
		{
			File.Delete(DbName);
			
			bool exists = false;			
			//bool exists = File.Exists(DbName);
			
			try
			{
				if ( !exists )
					SqliteConnection.CreateFile(DbName);
				
				_connection = new SqliteConnection("Data Source=" + DbName);
				
				if (!exists)
				{
					InitializeDatabase();
				}
				
				_connection.Open();
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);				
			}			
		}
	}
}

