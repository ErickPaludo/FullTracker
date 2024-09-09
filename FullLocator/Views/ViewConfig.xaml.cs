using FullLocator.Models.Armazenamento;
using FullLocator.ViewModels;

namespace FullLocator.Views;

public partial class ViewConfig : ContentPage
{
    private readonly IDataService _sqliteService;
    public ViewConfig(IDataService sqliteService)
    {
        InitializeComponent();
        _sqliteService = sqliteService;
        BindingContext = new ConfigVM(_sqliteService);
    }
}