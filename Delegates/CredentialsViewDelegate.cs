using System;
using MonoTouch.UIKit;

namespace theVault
{
	public class CredentialsViewDelegate : UITableViewDelegate
	{
		public override float GetHeightForHeader (UITableView tableView, int section)
		{
			return 0.0f;
		}	
	}
}

