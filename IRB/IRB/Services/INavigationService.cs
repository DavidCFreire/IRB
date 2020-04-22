using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.Services
{
    public interface INavigationService
    {
        void NavigateTo(object route);
        void NavigateModalToDocumentosPage();
        void NavigateToAbsolute(string route);
    }
}
