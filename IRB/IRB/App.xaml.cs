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
using System.Collections.Generic;
using IRB.Utils;
using Newtonsoft.Json;
using IRB.Models;

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
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IMessageService, MessageService>();
            vmLocator.LoadVM();
            InitializeComponent();
            CheckTheme();

            MainPage = new AppShell();
        }
        static bool DarkMode
        {
            get
            {
                return Preferences.Get(Settings.DarkMode, false);
            }
        }

        public static void CheckTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                string defaultJson = JsonConvert.SerializeObject(new ThemeModel() { Name = "Azul", Color = "#1976D2" });
                var savedJson = Preferences.Get(Settings.ThemeSelected, defaultJson);
                ThemeModel _selectedTheme = JsonConvert.DeserializeObject<ThemeModel>(savedJson);
                string ThemeName = _selectedTheme.Name;
                if (DarkMode)
                {
                    DependencyService.Get<IStatusBarStyleManager>().SetStatuBarBackground("#000000");
                    switch (ThemeName)
                    {
                        case "Azul":
                            mergedDictionaries.Add(new BlueDarkTheme());
                            break;
                        case "Vermelho":
                            mergedDictionaries.Add(new VermelhoDarkTheme());
                            break;
                        case "Marron":
                            mergedDictionaries.Add(new MarronDarkTheme());
                            break;
                        default:
                            mergedDictionaries.Add(new BlueDarkTheme());
                            break;
                    }
                }
                else
                {
                    DependencyService.Get<IStatusBarStyleManager>().SetStatuBarBackground(_selectedTheme.Color);
                    switch (ThemeName)
                    {
                        case "Azul":
                            mergedDictionaries.Add(new BlueLightTheme());
                            break;
                        case "Vermelho":
                            mergedDictionaries.Add(new VermelhoLightTheme());
                            break;
                        case "Marron":
                            mergedDictionaries.Add(new MarronLightTheme());
                            break;
                        default:
                            mergedDictionaries.Add(new BlueLightTheme());
                            break;
                    }
                }
            }
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
