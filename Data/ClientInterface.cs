using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace theVault
{
	public static class ClientInterface
	{
		private static VaultDatabase _database;
		private static ObservableCollection<Credential> _credentials;
		
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
		
		public static ObservableCollection<Credential> GetCredentials()
		{
			if (_credentials == null)
			{
				_credentials = new ObservableCollection<Credential>(_database.GetCredentials());
			}
			
			return _credentials;
		}		
		
		public static bool SaveCredential(Credential credential)
		{
			bool result = _database.SaveCredential(credential);
			
			if (result && _credentials.Contains(credential) == false)
				_credentials.Add(credential);
			
			return result;
		}
		
		#endregion
	}
}

