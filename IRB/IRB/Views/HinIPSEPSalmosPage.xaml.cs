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
    public partial class HinIPSEPSalmosPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;
        public HinIPSEPSalmosPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
        //protected async override void OnAppearing()
        //{
        //    await vm.SelecionarDocumento("Livro de Culto", "Saltério");
        //    base.OnAppearing();
        //}
    }
}