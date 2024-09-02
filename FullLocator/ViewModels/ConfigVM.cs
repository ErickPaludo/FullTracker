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
            try
            {
                Dados = new DataConfig(); // Inicializa Dados

                AddCommand = new Command(async () => await ExecuteAddCommand(service));
                UpdateCommand = new Command(async () => await ExecuteUpdateCommand(service));
                DeleteCommand = new Command(async () => await ExecuteDeleteCommand(service));
                DisplayCommand = new Command(async () => await Refresh(service));
            }
            catch (Exception ex)
            {
                // Logar ou exibir o erro
                System.Diagnostics.Debug.WriteLine($"Erro ao inicializar ConfigVM: {ex.Message}");
            }
        }

        private async Task ExecuteAddCommand(IDataService service)
        {
            await service.InitilizeAsync();
            await service.AddData(Dados);
            await Refresh(service);
        }

        private async Task ExecuteUpdateCommand(IDataService service)
        {
            await service.InitilizeAsync();
            await service.UpdateData(Dados);
            await Refresh(service);
        }

        private async Task ExecuteDeleteCommand(IDataService service)
        {
            await service.InitilizeAsync();

            var resposta = await App.Current.MainPage.DisplayAlert("Alerta", "Confirma exclusão?", "Sim", "Não");
            if (resposta)
            {
                await service.DeleteData(Dados);
            }

            await Refresh(service);
        }

        private async Task Refresh(IDataService service)
        {
            Data = await service.GetDataConfigAsync();
        }
    }
}
