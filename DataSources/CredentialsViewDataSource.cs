using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace theVault
{
	public class CredentialsViewDataSource : UITableViewDataSource
	{		
		private TableCellFactory<CredentialTableCell> factory = new TableCellFactory<CredentialTableCell>("CredentialCell", "CredentialTableCell");
		private List<Credential> _credentials;
		
		public CredentialsViewDataSource()
		{
			_credentials = ClientInterface.GetCredentials();
		}
			
	    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
	    {
			var cell = factory.GetCell(tableView);
			
			if ( cell != null )
	        	cell.SetupCell(_credentials[indexPath.Row]);
	        
	        return cell;
	    }
		
	    public override int NumberOfSections(UITableView tableView)
	    {
	        return 1;
	    }
	    
	    public override int RowsInSection(UITableView tableview, int section)
	    {
			return _credentials.Count;
	    }
		
	    public override string TitleForHeader(UITableView tableView, int section)
	    {
	        return "Section " + section;
	    }
	}

}

