using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace theVault
{
	public class CredentialHelper
	{
		public CredentialHelper ()
		{
		}
		
		public static void ShowCredentialDetails(Credential credential, UIViewController controller)
		{
			ShowCredentialDetails(credential, controller, null);
		}
		
		public static void ShowCredentialDetails(Credential credential, UIViewController controller, UITableView tableView)
		{
			bool isNew = credential.Id == 0;
			
			var binding = new BindingContext(null, credential, isNew ? "Add Credential" : credential.Name);
			var dialogViewController = new DialogViewController(binding.Root);

			if ( controller is UINavigationController )
			{
				var navController = (controller as UINavigationController);
										
				navController.PushViewController(dialogViewController, true);
				navController.NavigationBarHidden = false;
				
				UIBarButtonItem addButton = new UIBarButtonItem (UIBarButtonSystemItem.Save);
				dialogViewController.NavigationItem.HidesBackButton = false;
				dialogViewController.NavigationItem.RightBarButtonItem = addButton;
				
				addButton.Clicked += (sender, e) => 
				{
					binding.Fetch();
					binding.Root.Caption = credential.Name;
					
					if (ClientInterface.SaveCredential(credential)
					    && tableView != null)
					{
						tableView.ReloadData();
					}
					
					navController.PopViewControllerAnimated(true);
				};
			}
			else
			{
				controller.PresentModalViewController(dialogViewController, true);
			}						
		}
	}
}

