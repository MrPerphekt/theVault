using System;
using System.Collections.Generic;

namespace theVault
{
	public static class ClientInterface
	{
		private static VaultDatabase _database;
		
		static ClientInterface ()
		{
			InitializeDataAccess();
		}
		
		// Initialize the SQL database and/or create if needed
		private static void InitializeDataAccess()
		{
			_database =	new VaultDatabase();
		}
		
		#region Credentials
				
		public static void DeleteCredential(Credential credential)
		{
		}
		
		public static List<Credential> GetCredentials()
		{
			return _database.GetCredentials();
		}		
		
		public static bool SaveCredential(Credential credential)
		{
			return _database.SaveCredential(credential);
		}
		
		#endregion
	}
}

