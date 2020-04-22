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
    public partial class DocumentoEditarPage : ContentPage
    {
        public DocumentoEditarPage()
        {
            InitializeComponent();
            BindingContext = App.vmLocator.DocumentoEditar;
        }

        protected override void OnAppearing()
        {
            App.vmLocator.DocumentoEditar.Load();
            base.OnAppearing();
        }
    }
}