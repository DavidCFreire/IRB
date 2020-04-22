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
    public partial class HeidelbergIIPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;
        public HeidelbergIIPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
        protected async override void OnAppearing()
        {
            await vm.SelecionarDocumento("Catecismo de Heidelberg", "Parte II - Nossa Salvação");
            base.OnAppearing();
        }
    }
}