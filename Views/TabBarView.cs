// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using MonoTouch.ObjCRuntime;

namespace theVault
{
	public class TabBarItem
	{
		public UIButton Button { get; set; }
		public bool IsSelected { get; set; }
		public UIViewController ViewController { get; set; }
	}
	
	public partial class TabBarView : UIView
	{		
		private UIViewController _parentController;
		private List<UIViewController> _viewControllers;
		private List<TabBarItem> _views;
		private UIButton _actionButton;
		
		public TabBarView (IntPtr handle) : base (handle)
		{
			_views = new List<TabBarItem>();
			_viewControllers = new List<UIViewController>();
		}

		public void AddViewController(UIViewController viewController)
		{
			AddViewController(viewController, -1);
		}
		
		public void AddViewController(UIViewController viewController, int index)
		{
			if ( viewController == null )
				return;
			
			if ( _viewControllers.Contains(viewController) )
			{
				_viewControllers.Remove(viewController);
			}
			
			if ( index == -1 )
			{
				_viewControllers.Add(viewController);
			}
			else
			{
				if ( _viewControllers.Count > index )
				{
					_viewControllers.Insert(index, viewController);
				}
				else
				{
					_viewControllers.Add(viewController);
				}
			}
			
			UpdateViews();
		}
		
		private void Handle_ActionButtonTouchUpInside (object sender, EventArgs e)
		{
			var selectedView = _views.Where(v => v.IsSelected).FirstOrDefault();
			
			if (selectedView == null)
				return;
						
			var selector = new Selector("CreateNew");
			
			UIViewController selectedController = selectedView.ViewController;
			
			if (selectedView.ViewController is UINavigationController )
				selectedController = (selectedView.ViewController as UINavigationController).VisibleViewController;
			
			if (selectedController.CanPerform(selector, this))
				selectedController.PerformSelector(selector, this, 0f);
			else
				System.Diagnostics.Debug.WriteLine("{0} does not support CreateNew selector", selectedController);
		}		
		
		public override void LayoutSubviews ()
		{		
			//Determine center for action button location
			int actionButtonWidth = 44;
			int actionButtonHeight = 56;
			int actionButtonOffset = (int)(this.Frame.Width / 2) - actionButtonWidth / 2;
			
			int buttonWidth = (int)((this.Frame.Width - actionButtonWidth) / (_views.Count == 1 ? 2 : _views.Count));
			int buttonHeight = 44;
						
			for (int i = 0; i < _views.Count; i++)
			{
				var view = _views[i];				
				float offset = i * buttonWidth;
				
				if (offset >= actionButtonOffset)
					offset += actionButtonWidth;
				
				view.Button.Frame = new System.Drawing.RectangleF(offset, this.Frame.Height - buttonHeight, buttonWidth, buttonHeight);			
			}
			
			if (_actionButton != null )
			{
				_actionButton.Frame = new System.Drawing.RectangleF(actionButtonOffset, this.Frame.Height - actionButtonHeight, actionButtonWidth, actionButtonHeight);
			}
		}
		
		private void MakeViewActive(TabBarItem tabItem)
		{
			if ( tabItem == null || _parentController == null)
				return;
			
			var selectedView = _views.Where(v => v.IsSelected).FirstOrDefault();
						
			if ( selectedView == tabItem )
				return;
			
			tabItem.IsSelected = true;
			
			/*
			//This animates the entire window but works properly
			if (selectedView != null )				
				UIView.Transition(selectedView.ViewController.View, tabItem.ViewController.View, 1, 
				                  UIViewAnimationOptions.BeginFromCurrentState | UIViewAnimationOptions.CurveEaseInOut | UIViewAnimationOptions.TransitionCurlDown,
				                  () => 
				                 {  
									selectedView.ViewController.View.RemoveFromSuperview();
									selectedView.ViewController.RemoveFromParentViewController();
									tabItem.ViewController.View.Frame = new System.Drawing.RectangleF(0, 0, this.Frame.Width, _parentController.View.Frame.Height - this.Frame.Height);
									_parentController.View.InsertSubview(tabItem.ViewController.View, 0);
									_parentController.AddChildViewController(tabItem.ViewController);
								 });
			*/
			
			/*
			//This will animate specific area but doesn't use current state
			UIView.BeginAnimations("View Flip");
			UIView.SetAnimationDuration(1);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationTransition(UIViewAnimationTransition.CurlDown, tabItem.ViewController.View, true);
			UIView.SetAnimationBeginsFromCurrentState(true);
			*/
			
			//don't execute code below if using either animation
			
			tabItem.ViewController.ViewWillAppear(true);
			
			if (selectedView != null )
			{
				selectedView.ViewController.ViewWillDisappear(true);
				selectedView.ViewController.View.RemoveFromSuperview();
				selectedView.ViewController.RemoveFromParentViewController();
				selectedView.ViewController.ViewDidDisappear(true);
			}
			
			int width = 0;
			int height = 0;
			
			if ( UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Portrait ||
			    UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.PortraitUpsideDown ||
			    UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Unknown)
			{
				width = (int)_parentController.View.Frame.Width;
				height = (int)_parentController.View.Frame.Height;
			}
			else if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeLeft ||
			         UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeRight)
			{
				height = (int)_parentController.View.Frame.Width;
				width = (int)_parentController.View.Frame.Height;
			}
								
			tabItem.ViewController.View.Frame = new System.Drawing.RectangleF(0, 0, width, height - this.Frame.Height);
			_parentController.View.InsertSubview(tabItem.ViewController.View, 0);
			_parentController.AddChildViewController(tabItem.ViewController);
			
			tabItem.ViewController.ViewDidAppear(true);			

			//UIView.CommitAnimations();

			var tabItems = _views.Where (v => v != tabItem);
			
			foreach (var item in tabItems)
				item.IsSelected = false;
		}
		
		public void SelectView(int index)
		{
			if ( _views.Count > index )
			{
				MakeViewActive(_views[index]);
			}
		}
		
		public void SelectView(UIViewController controller)
		{
			if ( controller == null )
				return;
			
			var tabItem = _views.Where(v => v.ViewController == controller).FirstOrDefault();
			
			if ( tabItem == null )
				return;
			
			MakeViewActive(tabItem);
		}
			
		public void SetParentViewController(UIViewController controller)
		{
			_parentController = controller;
		}
		
		private void UpdateViews()
		{
			while (this.Subviews.Count() > 0)
				this.Subviews.Last().RemoveFromSuperview();
				
			//Remove old views
			var oldViews = _views.Where(v => _viewControllers.Contains(v.ViewController) == false).ToList();			
			oldViews.ForEach(v => _views.Remove(v));
				
			foreach (var viewController in _viewControllers)
			{
				var view = _views.Where(v => v.ViewController == viewController).FirstOrDefault();

				if ( view == null )
				{
					view = new TabBarItem();
					view.ViewController = viewController;
					view.Button = UIButton.FromType(UIButtonType.Custom);
					view.Button.BackgroundColor = UIColor.Clear;
					view.Button.TouchUpInside += (sender, e) => 
						{ 
							var button = sender as UIButton;
							
							if ( button == null ) return;
						
							var tabItem = _views.FirstOrDefault(v => v.Button == button);
						
							if ( tabItem != null)
								SelectView(tabItem.ViewController); 
						};
					
					_views.Insert(_viewControllers.IndexOf(viewController), view);
				}
			}
			
			int actionButtonWidth = 44;
			int actionButtonHeight = 56;
			int actionButtonOffset = (int)(this.Frame.Width / 2) - actionButtonWidth / 2;
			
			int buttonWidth = (int)((this.Frame.Width - actionButtonWidth) / (_views.Count == 1 ? 2 : _views.Count));
			int buttonHeight = 44;
						
			for (int i = 0; i < _views.Count; i++)
			{
				var view = _views[i];				
				float offset = i * buttonWidth;
				
				if (offset >= actionButtonOffset)
					offset += actionButtonWidth;
				
				view.Button.Frame = new System.Drawing.RectangleF(offset, this.Frame.Height - buttonHeight, buttonWidth, buttonHeight);
				view.Button.SetTitle(view.ViewController.Title, UIControlState.Normal);
				
				AddSubview(view.Button);
			}
			
			// Add action button
			if ( _actionButton == null )
			{			
				_actionButton = UIButton.FromType(UIButtonType.Custom);
				_actionButton.BackgroundColor = UIColor.Green;
				_actionButton.Frame = new System.Drawing.RectangleF(actionButtonOffset, this.Frame.Height - actionButtonHeight, actionButtonWidth, actionButtonHeight);
				_actionButton.TouchUpInside += Handle_ActionButtonTouchUpInside;
			}
			
			AddSubview(_actionButton);
			
			SetNeedsLayout();
		}
	}
}
