using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IRB.Views;
using IRB.Services;
using IRB.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace IRB
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address

        public static ViewModelLocator vmLocator = new ViewModelLocator();
        public App()
        {
            InitializeComponent();
            DependencyService.Register<INavigationService, NavigationService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=967e4c9e-be8c-48ef-981f-9e6b6975193d;" +
                              "uwp={Your UWP App secret here};" +
                              "ios={Your iOS App secret here}",
                              typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
