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
    public partial class ConfBelgaPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;

        public ConfBelgaPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
        protected async override void OnAppearing()
        {
            await vm.SelecionarDocumento("Confissão Belga");
            base.OnAppearing();
        }
    }
}