using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using IRB.iOS.Helpers;
using IRB.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarStyleManager))]
namespace IRB.iOS.Helpers
{
    public class StatusBarStyleManager : IStatusBarStyleManager
    {
        public void SetStatuBarBackground(string hexColor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if(hexColor == "#000000")
                {
                    UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.DarkContent, false);
                }
                else
                {
                    UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, false);
                }
                GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
            });
        }
        UIViewController GetCurrentViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;
            return vc;
        }
    }
}