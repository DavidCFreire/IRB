using FontAwesome;
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
    public partial class CredoApostolicoPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;
        public CredoApostolicoPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
        protected async override void OnAppearing()
        {
            await vm.SelecionarDocumento("Credos", "Apostólico", true);
            if (vm.LoggedUser != null && vm.LoggedUser.PK > 0 && (vm.LoggedUser.TIPO.ToLower() == "root" || vm.LoggedUser.TIPO.ToLower() == "editor"))
            {
                if(ToolbarItems.LastOrDefault().Text != "Editar Documento")
                {
                    FontImageSource fis = new FontImageSource()
                    {
                        Glyph = FontAwesomeIcons.Edit,
                        FontFamily = Device.RuntimePlatform == Device.iOS ? "FontAwesome5Free-Regular" : "FontAwesome5Regular.otf#Regular",
                        Size = 22
                    };
                    ToolbarItem item = new ToolbarItem
                    {
                        Text = "Editar Documento",
                        IconImageSource = fis,
                        Command = vm.NavigateToCommand,
                        CommandParameter = "documentos_editar",
                        Priority = 1
                    };
                    this.ToolbarItems.Add(item);
                }
            }
            else
            {
                var lastToolbar = ToolbarItems.LastOrDefault();
                if (lastToolbar.Text == "Editar Documento")
                {
                    this.ToolbarItems.Remove(lastToolbar);
                }
            }
            base.OnAppearing();
        }
        private async void TextOptionsGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsEnabled"))
            {
                if (TextOptionsGrid.IsEnabled)
                {
                    TextOptionsGrid.IsVisible = true;
                    AnimateIn();
                }
                else
                {
                    AnimateOut();
                    await Task.Delay(250);
                    TextOptionsGrid.IsVisible = false;
                }
            }
        }
        private void AnimateIn()
        {
            TextOptionsGrid.TranslateTo(0, 10, 1200, Easing.CubicOut);
            TextOptionsGrid.FadeTo(1, 250, Easing.SinInOut);
        }

        private void AnimateOut()
        {
            TextOptionsGrid.TranslateTo(0, TextOptionsGrid.Height, 1200, Easing.CubicOut);
            TextOptionsGrid.FadeTo(.01, 250, Easing.SinInOut);
        }

    }
}