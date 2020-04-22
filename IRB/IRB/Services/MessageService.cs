using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRB.Services
{
    public class MessageService : IMessageService
    {
        public void Alerta(string title, string text, string button)
        {
            try
            {
                App.Current.MainPage.DisplayAlert(title, text, button);
            }
            catch { }
        }

        public void Aviso(string text)
        {
            try
            {
                App.Current.MainPage.DisplayAlert("Aviso", text, "Ok");
            }
            catch { }
        }

        public async Task<bool> Pergunta(string title, string text, string btaccept, string btcancel)
        {
            try
            {
                if (await App.Current.MainPage.DisplayAlert(title, text, btaccept, btcancel))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
