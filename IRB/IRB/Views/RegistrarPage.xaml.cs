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
    public partial class RegistrarPage : ContentPage
    {

        public RegistrarPage()
        {
            InitializeComponent();
            BindingContext = App.vmLocator.Registrar;
        }
    }
}