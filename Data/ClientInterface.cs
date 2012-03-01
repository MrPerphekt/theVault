using System;
using System.Collections.Generic;

namespace theVault
{
	public static class ClientInterface
	{
		static ClientInterface ()
		{
			InitializeDataAccess();
		}
		
		// Initialize the SQL database and/or create if needed
		private static void InitializeDataAccess()
		{
			
		}
		
		#region Credentials
		
		// TODO:  Add support for creating a credential
		public static bool CreateCredential()
		{
			return true;
		}
		
		public static void DeleteCredential(Credential credential)
		{
		}
		
		public static List<Credential> GetCredentials()
		{
			return new List<Credential>();	
		}		
		
		#endregion
	}
}

