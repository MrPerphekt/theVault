using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.ObjectModel;

namespace theVault
{
	public class CredentialsViewDataSource : UITableViewDataSource, ISearchableDataSource
	{		
		private ObservableCollection<Credential> _credentials;
		private ObservableCollection<Credential> _filteredCredentials;
		
		public CredentialsViewDataSource()
		{
			_filteredCredentials = _credentials = ClientInterface.GetCredentials();
		}
			
		protected override void Dispose (bool disposing)
		{
			_filteredCredentials = null;
			_credentials = null;
		}

	    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
	    {
			string cellId = "CredentialTableCell";
									
			var credential = _filteredCredentials.OrderBy(c => c.Name).GroupBy(c => c.Name[0]).ElementAt(indexPath.Section).ElementAt(indexPath.Row);
 
			UITableViewCell cell = tableView.DequeueReusableCell(cellId);      
			
			if (cell == null )				
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellId);
 
			cell.TextLabel.Text = credential.Name;
			cell.DetailTextLabel.Text = credential.Url;
			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
			
			return cell;
	    }
		
		public Credential GetCredentialAtIndex(NSIndexPath indexPath)
		{
			Credential credential = null;
			
			var groups = _filteredCredentials.OrderBy(c => c.Name).GroupBy(c => c.Name[0]);
			
			if ( groups.Count() > indexPath.Section )
			{
				var groupData = groups.ElementAt(indexPath.Section);
				
				if (groupData.Count() > indexPath.Row)
					credential = groupData.ElementAt(indexPath.Row);	
			}
			
			return credential;
		}
		
	    public override int NumberOfSections(UITableView tableView)
	    {
	        return _filteredCredentials.OrderBy(c => c.Name).Select(c => !string.IsNullOrEmpty(c.Name) ? c.Name[0] : ' ').Distinct().Count();
	    }
	    
	    public override int RowsInSection(UITableView tableview, int section)
	    {
			var sections = _filteredCredentials.OrderBy(c => c.Name).Select(c => !string.IsNullOrEmpty(c.Name) ? c.Name[0] : ' ').Distinct();
			
			if ( sections.Count() > section )
			{
				var sectionLetter = sections.ElementAt(section);
				
				return _filteredCredentials.OrderBy(c => c.Name).Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.StartsWith(sectionLetter.ToString())).Count();
			}
			
			return 0;
	    }
		
		public void Search(string searchText)
		{
			if ( string.IsNullOrEmpty(searchText))
				_filteredCredentials = _credentials;
			else
				_filteredCredentials = new ObservableCollection<Credential>(_credentials.Where (c => c.Name.Contains(searchText)).OrderBy(c => c.Name).ToList());
		}
		
		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return new string[] { "{search}", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };			
			//return _credentials.Select(c => !string.IsNullOrEmpty(c.Name) ? c.Name[0].ToString() : "").Distinct().ToArray();
		}
		
	    public override string TitleForHeader(UITableView tableView, int section)
	    {
			var sections = _filteredCredentials.OrderBy(c => c.Name).Select(c => !string.IsNullOrEmpty(c.Name) ? c.Name[0] : ' ').Distinct();
			
			if ( sections.Count() > section )
			{
				return sections.ElementAt(section).ToString();
			}
			
			return null;
	    }
		
	}

}

