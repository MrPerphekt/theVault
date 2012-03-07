using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace theVault
{
	public class TableCellFactory<T> where T : UITableViewCell
	{
	    private string cellId;
	    private string nibName;
	    
	    public TableCellFactory(string cellId, string nibName)
	    {
	        this.cellId = cellId;
	        this.nibName = nibName;
	    }
	    
	    public T GetCell(UITableView tableView)
	    {
	        var cell = tableView.DequeueReusableCell(cellId) as T;
	            
	        if (cell == null)
	        {
	            cell = Activator.CreateInstance<T>();
	            var views = NSBundle.MainBundle.LoadNib(nibName, cell, null);
	            cell = Runtime.GetNSObject( views.ValueAt(0) ) as T;
	        }
	        
	        return cell;
	    }
	}
}

