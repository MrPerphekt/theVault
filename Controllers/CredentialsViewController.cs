using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace theVault
{
	public partial class CredentialsViewController : UITableViewController
	{
		public override string Title 
		{
			get 
			{
				return NSBundle.MainBundle.LocalizedString ("Credentials", "Credentials");
			}
		}

		public CredentialsViewController (IntPtr handle) 
			: base (handle)
		{
		}
		
		public CredentialsViewController ()
			: base ("CredentialsViewController", null)
		{
		}
		
		#region View lifecycle
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
					
			this.TableView.Delegate = new CredentialsViewDelegate();
			this.TableView.DataSource = new CredentialsViewDataSource();
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();

			this.TableView.Delegate.Dispose();
			this.TableView.Delegate = null;
			
			this.TableView.DataSource.Dispose();
			this.TableView.DataSource = null;
						
			ReleaseDesignerOutlets ();
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}
		
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}
		
		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}
		
		#endregion
				
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{		
			return true;
		}
	}
}

