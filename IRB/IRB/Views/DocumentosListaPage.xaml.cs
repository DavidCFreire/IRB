using IRB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IRB.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentosListaPage : ContentPage
    {
        public DocumentosListaPage()
        {
            InitializeComponent();
            BindingContext = App.vmLocator.Documentos;
        }
    }
}