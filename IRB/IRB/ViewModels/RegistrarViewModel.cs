using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class RegistrarViewModel : BaseViewModel
    {
        public RegistrarViewModel()
        {

        }
        private string _usuario;
        public string Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _senha;
        public string Senha
        {
            get => _senha;
            set => SetProperty(ref _senha, value);
        }
        private string _senhaRepetir;
        public string SenhaRepetir
        {
            get => _senhaRepetir;
            set => SetProperty(ref _senhaRepetir, value);
        }
        //ViewPassword
        private bool _viewPassword = false;
        public bool ViewPassword
        {
            get => _viewPassword;
            set => SetProperty(ref _viewPassword, value);
        }
        public ICommand RegistrarCommand
        {
            get
            {
                return new Command(async() => 
                {
                    if (IsBusy)
                        return;
                    IsBusy = true;

                    if (Senha != SenhaRepetir)
                    {
                        _messageService.Aviso("Senhas Não Coincidem");
                        IsBusy = false;
                        return;
                    }
                    try
                    {
                        var email = await API.GetUsuarioByEmail(Email);
                        if (email != null && email.PK > 0)
                        {
                            _messageService.Aviso("Email Já Cadastrado");
                            IsBusy = false;
                            return;
                        }
                    }
                    catch { }
                    try
                    {
                        var usuario = await API.GetUsuarioByUsername(Usuario);
                        if (usuario != null && usuario.PK > 0)
                        {
                            _messageService.Aviso("Usuario Já Cadastrado");
                            IsBusy = false;
                            return;
                        }
                    }
                    catch { }
                    if (await _messageService.Pergunta("Registrar-se", "Deseja Registrar-se agora?", "Registrar", "Cancelar"))
                    {
                        Data.Usuario user = new Data.Usuario()
                        {
                            EMAIL = Email,
                            SENHA = Senha,
                            USUARIO = Usuario,
                            TIPO = "User"
                        };
                        await API.CreateUsuario(user);
                        IsBusy = false;
                        Navigate("..");
                    }

                    IsBusy = false;
                });
            }
        }
    }
}
