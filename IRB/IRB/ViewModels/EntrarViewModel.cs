using IRB.Data;
using IRB.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class EntrarViewModel : BaseViewModel
    {
        public EntrarViewModel()
        {

        }

        private string _usuario;
        public string Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }
        private string _senha;
        public string Senha
        {
            get => _senha;
            set => SetProperty(ref _senha, value);
        }
        private bool _viewPassword = true;
        public bool ViewPassword
        {
            get => _viewPassword;
            set => SetProperty(ref _viewPassword, value);
        }
        public ICommand ViewPasswordCommand
        {
            get
            {
                return new Command(() =>
                {
                    ViewPassword = !ViewPassword;
                });
            }
        }
        public ICommand EntrarCommand
        {
            get
            {
                return new Command(async() =>
                {
                    if (IsBusy)
                        return;

                    IsBusy = true;
                    try
                    {
                        Usuario usuario = await API.GetUsuarioByUsernameSenha(Usuario, Senha);
                        if (usuario.PK != 0)
                        {
                            var usuariojson = JsonConvert.SerializeObject(usuario);
                            Preferences.Set(Settings.UsuarioLogged, usuariojson);
                            Usuario = string.Empty;
                            Senha = string.Empty;
                            App.vmLocator.Perfil.UpdateView();
                            Navigate("..");
                        }
                        else
                        {
                            _messageService.Alerta("Entrar", "Usuario não encontrado", "Tentar Novamente");
                        }
                    }
                    catch
                    {
                        _messageService.Alerta("Entrar", "Usuario não encontrado", "Tentar Novamente");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }
    }
}
