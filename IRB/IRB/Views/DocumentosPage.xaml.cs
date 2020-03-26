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
    public partial class DocumentosPage : ContentPage
    {
        public DocumentosViewModel vm = App.vmLocator.Documentos;
        public DocumentosPage()
        {
            InitializeComponent();
            BindingContext = vm;
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
            TextOptionsGrid.TranslateTo(0, 0, 1200, Easing.SpringOut);
            TextOptionsGrid.FadeTo(1, 250, Easing.SinInOut);
        }

        private void AnimateOut()
        {
            TextOptionsGrid.TranslateTo(0, TextOptionsGrid.Height, 1200, Easing.SpringOut);
            TextOptionsGrid.FadeTo(.01, 250, Easing.SinInOut);
        }
    }
}