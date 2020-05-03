using FontAwesome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class InicioViewModel : BaseViewModel
    {
        public InicioViewModel()
        {

        }

        public DateTime Data
        {
            get => DateTime.Now;
        }

        public string Saudacao
        {
            get
            {
                string ret = "Boa Noite";
                if (Data.Hour > 4)
                    ret = "Bom dia";

                if (Data.Hour > 11)
                    ret = "Boa tarde";

                if (Data.Hour > 18)
                    ret = "Boa noite";

                if (LoggedUser != null && LoggedUser.PK > 0)
                    ret = ret + ", " + LoggedUser.USUARIO;

                return ret;
            }
        }

        public void UpdateView()
        {
            OnPropertyChanged(nameof(Saudacao));
            OnPropertyChanged(nameof(Data));
            OnPropertyChanged(nameof(SyncImage));
            OnPropertyChanged(nameof(SyncMessage));
            OnPropertyChanged(nameof(SyncColor));
        }

        public string SyncColor
        {
            get
            {
                string color = "#000000";
                if (DocumentosUpdateAvailable)
                {
                    //VERMELHO
                    color = "#D32F2F";
                }
                else
                {
                    //VERDE
                    color = "#388E3C";
                }
                return color;
            }
        }

        public string SyncMessage
        {
            get
            {
                if (DocumentosUpdateAvailable)
                {
                    return "Existe Atualização de Documentos!";
                }
                else
                {
                    return "Documentos Atualizados!";
                }
            }
        }
        public string SyncImage
        {
            get
            {
                if (DocumentosUpdateAvailable)
                {
                    return FontAwesomeIcons.ExclamationTriangle;
                }
                else
                {
                    return FontAwesomeIcons.CheckCircle;
                }
            }
        }

        public ICommand AtualizarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await DownloadDocumentos();
                    OnPropertyChanged(nameof(SyncImage));
                    OnPropertyChanged(nameof(SyncMessage));
                    OnPropertyChanged(nameof(SyncColor));
                });
            }
        }

    }
}
