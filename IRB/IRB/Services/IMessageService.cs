using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRB.Services
{
    public interface IMessageService
    {
        void Aviso(string text);
        void Alerta(string title, string text, string button);
        Task<bool> Pergunta(string title, string text, string btaccept, string btcancel);
    }
}
