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
    public partial class DortVPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;
        public DortVPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
        protected async override void OnAppearing()
        {
            await vm.SelecionarDocumento("Cânones de Dort", "V Capítulo: A Perseverança dos Santos");
            base.OnAppearing();
        }
    }
}