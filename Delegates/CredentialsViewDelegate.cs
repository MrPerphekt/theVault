using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace theVault
{
	public class CredentialsViewDelegate : UITableViewDelegate
	{
		private UIViewController _controller;
		
		public CredentialsViewDelegate(UIViewController controller)
		{
			_controller = controller;
		}
		
		public override void AccessoryButtonTapped (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			if (_controller == null)
				return;
			
			var dataSource = tableView.DataSource as CredentialsViewDataSource;
			
			if (dataSource != null)
			{
				Credential credential = dataSource.GetCredentialAtIndex(indexPath);
				
				if ( credential != null )
				{
					CredentialHelper.ShowCredentialDetails(credential, _controller, tableView);
				}
			}
		}
	}
}

