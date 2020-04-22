using IRB.Data;
using IRB.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class PerfilViewModel : BaseViewModel
    {
        public PerfilViewModel()
        {

        }

        public string Usuario
        {
            get
            {
                if (LoggedIn)
                    return LoggedUser.USUARIO;
                else
                    return "Entrar";
            }
        }

        public string Email
        {
            get
            {
                if (LoggedIn)
                    return LoggedUser.EMAIL;
                else
                    return string.Empty;
            }
        }

        public bool LoggedIn
        {
            get
            {
                if (LoggedUser.PK == 0)
                    return false;
                else
                    return true;
            }
        }

        public ICommand PerfilCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (LoggedIn)
                    {
                    }
                    else
                    {
                        _navigationService.NavigateTo("entrar");
                    }
                });
            }
        }
        public ICommand SairCommand
        {
            get
            {
                return new Command(async() =>
                {
                    if (LoggedIn)
                    {
                        if(await _messageService.Pergunta("Sair", "Deseja Realmente Sair?", "Sair", "Cancelar"))
                        {
                            Preferences.Set(Settings.UsuarioLogged, string.Empty);
                            UpdateView();
                        }
                    }
                    else { }
                });
            }
        }

        public void UpdateView()
        {
            OnPropertyChanged(nameof(LoggedIn));
            OnPropertyChanged(nameof(Usuario));
            OnPropertyChanged(nameof(Email));

        }
    }
}
