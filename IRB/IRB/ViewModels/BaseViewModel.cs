using IRB.Services;
using IRB.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public IRestApi API = RestService.For<IRestApi>(EndPoints.BaseUrl);

        protected INavigationService _navigationService;
        public BaseViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();

        }
        public ICommand NavigateToCommand => new Command(Navigate);

        public void Navigate(object route)
        {
            _navigationService.NavigateTo(route);
        }

        public ICommand NavigateToAbsoluteCommand => new Command((route) => NavigateAbsolute(route.ToString()));

        public void NavigateAbsolute(string route)
        {
            _navigationService.NavigateToAbsolute(route);
        }

        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public void RegisterRoutes()
        {
            routes.Add("inicio", typeof(InicioPage));
            routes.Add("documentos", typeof(DocumentosPage));
            routes.Add("pesquisa", typeof(PesquisaPage));
            routes.Add("eventos", typeof(EventosPage));
            routes.Add("perfil", typeof(PerfilPage));
            routes.Add("documentos_lista", typeof(DocumentosListaPage));
            routes.Add("capitulos_lista", typeof(CapitulosListaPage));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
