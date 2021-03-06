﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IRB.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PesquisaPage : ContentPage
    {
        public PesquisaPage()
        {
            InitializeComponent();
            BindingContext = App.vmLocator.Pesquisa;
        }

        protected async override void OnAppearing()
        {
            await App.vmLocator.Pesquisa.LoadLivros();
            base.OnAppearing();
        }
    }
}