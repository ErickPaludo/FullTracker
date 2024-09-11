using FullLocator.Models.Armazenamento;
using FullLocator.ViewModels;
using FullLocator.Views.Menu;

namespace FullLocator
{
    public partial class App : Application
    {
        public App(IDataService dataservice)
        {
            InitializeComponent();
            new HomeVM(dataservice);
            MainPage = new NavigationPage(new ViewMenu(dataservice));
        }
    }
}
