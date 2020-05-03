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
    public partial class DortIIPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;
        public DortIIPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
        //protected async override void OnAppearing()
        //{
        //    await vm.SelecionarDocumento("Cânones de Dort", "II Capítulo: A Morte de Cristo e a Redenção do Homem Através Dela");
        //    base.OnAppearing();
        //}
    }
}