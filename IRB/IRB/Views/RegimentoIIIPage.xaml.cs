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
    public partial class RegimentoIIIPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;
        public RegimentoIIIPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
        protected async override void OnAppearing()
        {
            await vm.SelecionarDocumento("Regimento", "III. Os Cultos Públicos E Os Sacramentos");
            base.OnAppearing();
        }

    }
}