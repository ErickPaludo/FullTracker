using CommunityToolkit.Mvvm.ComponentModel;
using FullLocator.Models;
using FullLocator.Models.Armazenamento;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullLocator.ViewModels
{
    public partial class ConfigVM : ObservableObject
    {
        private readonly IDataService _dataService;
        [ObservableProperty]
        private List<DataConfig> _data;

        [ObservableProperty]
        private DataConfig _dados; // Propriedade com notificação de mudança

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand DisplayCommand { get; }

        public ConfigVM()
        {
        }

        public ConfigVM(IDataService service)
        {
            Dados = new DataConfig();
            AddCommand = new Command(async () =>
            {
                await service.InitilizeAsync();
                await service.AddData(Dados);
                await Refresh(service);
            });

            UpdateCommand = new Command(async () =>
            {
                await service.InitilizeAsync();
                await service.UpdateData(Dados);
                await Refresh(service);
            });

            DeleteCommand = new Command(async () =>
            {
                await service.InitilizeAsync();

                var resposta = await App.Current.MainPage.DisplayAlert("Alerta", "Confirma exclusão ?", "Sim", "Não");
                if (resposta)
                    await service.DeleteData(Dados);

                await Refresh(service);
            });

            DisplayCommand = new Command(async () =>
            {
                await service.InitilizeAsync();
                await Refresh(service);
            });
        }

        private async Task Refresh(IDataService service)
        {
            Data = await service.GetDataConfigAsync();
        }
    }
}
