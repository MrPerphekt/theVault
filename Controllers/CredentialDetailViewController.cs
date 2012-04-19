using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace theVault
{
	public partial class CredentialDetailViewController : UIViewController
	{
		public Credential ActiveCredential
		{
			get;
			set;			
		}
		
		public CredentialDetailViewController () 
			: this(null)
		{
		}
		
		public CredentialDetailViewController (Credential credential)
			: base ("CredentialDetailViewController", null)			
		{
			ActiveCredential = credential;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			if ( NavigationController != null )
			{
				NavigationController.NavigationBarHidden = false;
			}
			
			if ( ActiveCredential == null )
			{
				this.Title = "Add Credential";
				ActiveCredential = new Credential();
			}
			else
			{
				this.Title = ActiveCredential.Name;
			}			
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

