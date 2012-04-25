using System;
using MonoTouch.Dialog;

namespace theVault
{
	public class Credential
	{
		public int Id { get; set; }
		
		[Section("Site Information")]
    	[Entry("Enter site name")]
		public string Name { get; set; }

    	[Entry("Enter site url")]
		public string Url { get; set; }
				
		[Section("Account Infomation")]
    	[Entry("Enter your username")]
		public string Username { get; set; }		

		[Caption("Password"), Password("Enter your password")]
		public string Password { get; set; }
		
		[Entry("Enter notes")]
		public string Notes { get; set; }
		
		public Credential ()
		{
		}
	}
}

