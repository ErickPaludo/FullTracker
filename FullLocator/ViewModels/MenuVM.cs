using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FullLocator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;
using ProjetoMuai;
using FullLocator.Views;

namespace FullLocator.ViewModels
{
    public partial class MenuVM : ObservableObject
    {
        private readonly INavigation _navigation;

        public MenuVM(INavigation navigation)
        {
            _navigation = navigation;
        }

        public MenuVM()
        {
        }

        [RelayCommand]
        private async Task Config() 
        {
            var viewHomePage = new ViewHome();

            // Navega para a ViewHome
            await _navigation.PushAsync(viewHomePage);
        }
    }
}
