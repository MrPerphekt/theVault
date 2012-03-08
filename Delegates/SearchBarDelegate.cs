using System;
using MonoTouch.UIKit;

namespace theVault
{
	public class SearchBarDelegate : UISearchBarDelegate
	{
		private UITableViewController _tableViewController;
		
		public SearchBarDelegate (UITableViewController controller)
		{
			_tableViewController = controller;
		}		
		
		public override void TextChanged (UISearchBar searchBar, string searchText)
		{
			if ( _tableViewController != null && _tableViewController.TableView != null)
			{
				var dataSource = _tableViewController.TableView.DataSource as ISearchableDataSource;
				
				if (dataSource != null)
				{
					dataSource.Search(searchText);
					_tableViewController.TableView.ReloadData();
				}
			}
		}
	}
}

