using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace theVault
{
	public partial class CredentialsViewController : UITableViewController
	{
		public CredentialsViewController (IntPtr handle) 
			: base (handle)
		{
			Initialize();			
		}
		
		public CredentialsViewController ()
			: base ("CredentialsViewController", null)
		{
			Initialize();
		}
		
		[Export("CreateNew")]
		public void CreateNew()		
		{
			System.Diagnostics.Debug.WriteLine("Executed Create New on CredentialsViewController");
			
			CredentialHelper.ShowCredentialDetails(new Credential(), NavigationController == null ? (UIViewController)this : (UIViewController)NavigationController, TableView);
		}
		
		private void Initialize()
		{
			this.Title = NSBundle.MainBundle.LocalizedString ("Credentials", "Credentials");		
		}
				
		#region View lifecycle
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
								
			this.Title = NSBundle.MainBundle.LocalizedString ("Credentials", "Credentials");		

			this.TableView.Delegate = new CredentialsViewDelegate(this.NavigationController == null ? this : (UIViewController)NavigationController);
			this.TableView.DataSource = new CredentialsViewDataSource();
			
			_searchBar.Delegate = new SearchBarDelegate(this);
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
			if ( NavigationController != null )
			{
				NavigationController.NavigationBarHidden = true;
			}
			
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

