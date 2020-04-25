using IRB.Models;
using IRB.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class TemaViewModel : BaseViewModel
    {
        public TemaViewModel()
        {
            Load();
        }

        private void Load()
        {
            try
            {
                ObservableCollection<ThemeModel> load = new ObservableCollection<ThemeModel>();
                    //#5D4037
                load.Add(new ThemeModel() { Name = "Vermelho", Color = "#D32F2F" });
                load.Add(new ThemeModel() { Name = "Azul", Color = "#1976D2" });
                load.Add(new ThemeModel() { Name = "Marron", Color = "#5D4037" });
                string defaultJson = JsonConvert.SerializeObject(new ThemeModel() { Name = "Azul", Color = "#1976D2" });
                var savedJson = Preferences.Get(Settings.ThemeSelected, defaultJson);
                _selectedTheme = JsonConvert.DeserializeObject<ThemeModel>(savedJson);
                foreach (var item in load)
                {
                    if(item.Name == _selectedTheme.Name)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
                ListThemes = new ObservableCollection<ThemeModel>(load);
            }
            catch { }
        }

        private ObservableCollection<ThemeModel> _listThemes = new ObservableCollection<ThemeModel>();
        public ObservableCollection<ThemeModel> ListThemes
        {
            get { return _listThemes ?? (_listThemes = new ObservableCollection<ThemeModel>()); }
            set { SetProperty(ref _listThemes, value); }
        }

        private ThemeModel _selectedTheme;
        public ThemeModel SelectedTheme
        {
            get
            {
                return null;
            }
            set
            {
                try
                {
                    string itemJson = JsonConvert.SerializeObject(value);
                    Preferences.Set(Settings.ThemeSelected, itemJson);
                    Load();
                    App.CheckTheme();
                }
                catch { }
                OnPropertyChanged(nameof(SelectedTheme));
            }
        }
        public bool DarkMode
        {
            get
            {
                return Preferences.Get(Settings.DarkMode, false);
            }
            set
            {
                Preferences.Set(Settings.DarkMode, value);
                OnPropertyChanged(nameof(DarkMode));
                App.CheckTheme();
            }
        }
    }
}
