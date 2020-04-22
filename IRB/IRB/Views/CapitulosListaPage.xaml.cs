﻿using IRB.ViewModels;
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
    public partial class CapitulosListaPage : ContentPage
    {
        DocumentosViewModel vm = App.vmLocator.Documentos;

        public CapitulosListaPage()
        {
            InitializeComponent();
            BindingContext = vm;
            
        }
    }
}