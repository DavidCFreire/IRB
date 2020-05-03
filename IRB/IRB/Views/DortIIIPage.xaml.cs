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
    public partial class DortIIIPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;
        public DortIIIPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
        //protected async override void OnAppearing()
        //{
        //    await vm.SelecionarDocumento("Cânones de Dort", "III - IV Capítulo: A Corrupção do Homem, a sua Conversão a Deus e o Modo como isso Ocorre");
        //    base.OnAppearing();
        //}
    }
}