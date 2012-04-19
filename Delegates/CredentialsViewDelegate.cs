using System;
using MonoTouch.UIKit;

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
					CredentialDetailViewController controller = new CredentialDetailViewController(credential);
					
					if ( _controller is UINavigationController )
						(_controller as UINavigationController).PushViewController(controller, true);
					else
						_controller.PresentModalViewController(controller, true);					
				}
			}
		}
	}
}

