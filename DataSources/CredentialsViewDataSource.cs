using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace theVault
{
	public class CredentialsViewDataSource : UITableViewDataSource
	{		
		private List<Credential> _credentials;
		
		public CredentialsViewDataSource()
		{
			_credentials = ClientInterface.GetCredentials();
		}
			
		protected override void Dispose (bool disposing)
		{
			_credentials = null;
		}

	    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
	    {
			string cellId = "CredentialTableCell";
									
			var credential = _credentials.GroupBy(c => c.Name[0]).ElementAt(indexPath.Section).ElementAt(indexPath.Row);
 
			UITableViewCell cell = tableView.DequeueReusableCell(cellId);      
			
			if (cell == null )				
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellId);
 
			cell.TextLabel.Text = credential.Name;
			cell.DetailTextLabel.Text = credential.Url;
			//cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
			
			return cell;
	    }
		
	    public override int NumberOfSections(UITableView tableView)
	    {
	        return _credentials.Select(c => !string.IsNullOrEmpty(c.Name) ? c.Name[0] : ' ').Distinct().Count();
	    }
	    
	    public override int RowsInSection(UITableView tableview, int section)
	    {
			var sections = _credentials.Select(c => !string.IsNullOrEmpty(c.Name) ? c.Name[0] : ' ').Distinct();
			
			if ( sections.Count() > section )
			{
				var sectionLetter = sections.ElementAt(section);
				
				return _credentials.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.StartsWith(sectionLetter.ToString())).Count();
			}
			
			return 0;
	    }
		
		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return new string[] { "{search}", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };			
			//return _credentials.Select(c => !string.IsNullOrEmpty(c.Name) ? c.Name[0].ToString() : "").Distinct().ToArray();
		}
		
	    public override string TitleForHeader(UITableView tableView, int section)
	    {
			var sections = _credentials.Select(c => !string.IsNullOrEmpty(c.Name) ? c.Name[0] : ' ').Distinct();
			
			if ( sections.Count() > section )
			{
				return sections.ElementAt(section).ToString();
			}
			
			return null;
	    }
		
	}

}

