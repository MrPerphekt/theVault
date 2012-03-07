// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace theVault
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		theVault.TabBarView _tabBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_tabBar != null) {
				_tabBar.Dispose ();
				_tabBar = null;
			}
		}
	}
}
