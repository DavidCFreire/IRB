using IRB.Data;
using IRB.Services;
using IRB.Utils;
using IRB.Views;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private DBHelper _dbHelper;
        public DBHelper DbHelper
        {
            get
            {
                if (_dbHelper == null)
                    _dbHelper = new DBHelper();

                return _dbHelper;
            }
            set
            {
                _dbHelper = value;
                OnPropertyChanged(nameof(DbHelper));
            }
        }

        public Usuario LoggedUser
        {
            get
            {
                try
                {
                    Usuario ret = new Usuario();
                    try
                    {
                        ret = JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Settings.UsuarioLogged, string.Empty));
                    }
                    catch { }
                    if (ret == null)
                        ret = new Usuario();
                    return ret;
                }
                catch
                {
                    return new Usuario();
                }
            }
        }

        public bool ModoOffline
        {
            get
            {
                return Preferences.Get(Settings.ModoOffline, false);
            }
            set
            {
                Preferences.Set(Settings.ModoOffline, value);
                if (value)
                    DownloadDocumentos();
                else
                    Preferences.Set(Settings.ModoAutoSync, false);
                OnPropertyChanged(nameof(ModoOffline));
                OnPropertyChanged(nameof(ModoAutoSync));
            }
        }

        public bool ModoAutoSync
        {
            get
            {
                return Preferences.Get(Settings.ModoAutoSync, false);
            }
            set
            {
                if (ModoOffline)
                    Preferences.Set(Settings.ModoAutoSync, value);
                else
                    _messageService.Aviso("Modo Offline precisa estar ativado");
                OnPropertyChanged(nameof(ModoAutoSync));
            }
        }

        public bool DocumentosUpdateAvailable
        {
            get
            {
                return Preferences.Get(Settings.DocumentosUpdateAvailable, false);
            }
            set
            {
                Preferences.Set(Settings.DocumentosUpdateAvailable, value);
                OnPropertyChanged(nameof(DocumentosUpdateAvailable));
            }
        }

        public async Task DownloadDocumentos()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            var connectivity = Connectivity.NetworkAccess;
            if (connectivity == NetworkAccess.Internet)
            {
                IEnumerable<Documento> documentos = await API.GetAllDocumentos();
                DbHelper.InsertBulkDocumentos(documentos);
                DocumentosUpdateAvailable = false;
            }
            else
            {
                _messageService.Aviso("Você não está com acesso a internet");
            }
            IsBusy = false;
        }
        public async void SyncDocumentos(bool showAviso = false)
        {
            var connectivity = Connectivity.NetworkAccess;
            if(connectivity == NetworkAccess.Internet)
            {
                if (LoggedUser != null && LoggedUser.PK > 0)
                {
                    var list = DbHelper.GetAllDocumentos();
                    IEnumerable<Documento> listUpdate = new List<Documento>(list.Where(x => x.VERSAO == 0));
                    if(listUpdate != null && listUpdate.Count() > 0)
                    {
                        foreach (var item in listUpdate)
                        {
                            await API.UpdateDocumento(item.PK, item);
                        }
                    }
                }

                if (ModoOffline)
                {
                    var list = DbHelper.GetAllDocumentos();
                    if(list != null && list.Count > 0)
                    {
                        int version = list.Max(x => x.VERSAO);
                        IEnumerable<Documento> documentos = await API.GetDocumentosByVersion(version);
                        if (documentos != null && documentos.Count() > 0)
                        {
                            if (ModoAutoSync)
                            {
                                DbHelper.InsertBulkDocumentos(documentos);
                                DocumentosUpdateAvailable = false;
                            }
                            else
                            {
                                DocumentosUpdateAvailable = true;
                            }
                        }
                    }
                }
            }
            else
            {
                if(showAviso)
                    _messageService.Aviso("Você não está com acesso a internet, o documento atualizado será enviado quando o app tiver acesso a internet.");
            }
        }

        public IRestApi API = RestService.For<IRestApi>(EndPoints.BaseUrl);

        protected INavigationService _navigationService;
        protected IMessageService _messageService;
        public BaseViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            SyncDocumentos();
        }
        public ICommand MessageCommand => new Command(Message);
        public void Message(object text)
        {
            _messageService.Alerta("Info", text.ToString(), "Ok");
        }
        public ICommand NavigateToCommand => new Command(Navigate);
        public ICommand NavigateModalDocumentosCommand => new Command(NavigateModalDocumentos);

        public void NavigateModalDocumentos()
        {
            _navigationService.NavigateModalToDocumentosPage();
        }

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
            routes.Add("documentos_editar", typeof(DocumentoEditarPage));
            routes.Add("capitulos_lista", typeof(CapitulosListaPage));
            routes.Add("entrar", typeof(EntrarPage));
            routes.Add("registrar", typeof(RegistrarPage));

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
