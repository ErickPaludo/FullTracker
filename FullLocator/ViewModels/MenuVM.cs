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
using FullLocator.Models.Armazenamento;

namespace FullLocator.ViewModels
{
    public partial class MenuVM : ObservableObject
    {
        private  readonly INavigation _navigation;
        private static IDataService _service;
        public MenuVM(INavigation navigation)
        {
            _navigation = navigation;
        }
        public MenuVM(IDataService service)
        {
            _service = service;
        }

        public MenuVM()
        {
        }

        [RelayCommand]
        private async Task Config() 
        {
            NavigationPage navpage = (NavigationPage)App.Current.MainPage;
            navpage.PushAsync(new ViewConfig(_service));
        }
    }
}
