using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace IRB.Services
{
    public class NavigationService : INavigationService
    {
        public async void NavigateTo(object route)
        {
            if(route.ToString() == "..")
            {
                await Shell.Current.Navigation.PopToRootAsync();
            }
            else
            {
                ShellNavigationState state = Shell.Current.CurrentState;
                if (state.Location.ToString().EndsWith(route.ToString()))
                    return;

                await Shell.Current.GoToAsync($"{state.Location}/{route.ToString()}");
                //Shell.Current.FlyoutIsPresented = false;
            }
        }

        public async void NavigateToPassing(object route, object viewModel)
        {
            ShellNavigationState state = Shell.Current.CurrentState;

            await Shell.Current.GoToAsync($"{state.Location}/{route.ToString()}?viewModel={viewModel}");
        }
        public async void NavigateToAbsolute(string route)
        {
            ShellNavigationState state = Shell.Current.CurrentState;

            await Shell.Current.GoToAsync(route);
        }
    }
}
