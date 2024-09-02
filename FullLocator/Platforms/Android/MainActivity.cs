using Android.App;
using Android.Content.PM;
using Android.OS;

namespace FullLocator
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static MainActivity ActivityCurrent
        {
            get; set;
        }
        public MainActivity()
        {
            ActivityCurrent = this;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Solicitar permissões de localização
            RequestLocationPermissionAsync();
        }
        private async Task RequestLocationPermissionAsync()
        {
            // Verifique se os dados ou configurações necessários estão presentes
            if (!Preferences.ContainsKey("SomeRequiredSetting"))
            {
                // Redefina a configuração ou solicite a entrada do usuário
                Preferences.Set("SomeRequiredSetting", "defaultValue");
            }

            PermissionStatus locationStatus;

            while (true)
            {
                // Solicita a permissão de localização
                locationStatus = await Permissions.RequestAsync<Permissions.LocationAlways>();

                if (locationStatus == PermissionStatus.Granted)
                {
                    break;
                }
                else
                {
                    // Exibe um pop-up perguntando se o usuário tem certeza que deseja negar a permissão
                    bool confirmExit = await DisplayAlert(
                        "Permissão Necessária",
                        "Você negou a permissão de localização. Tem certeza de que deseja continuar sem concedê-la?",
                        "Sim",
                        "Não"
                    );

                    if (confirmExit)
                    {
                        FinishAffinity();
                        return;
                    }
                }
            }
            while (true)
            {
                var storageStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (storageStatus == PermissionStatus.Granted)
                {
                    break;
                }
                else
                {
                    bool confirmExit = await DisplayAlert(
                        "Permissão Necessária",
                        "Você negou a permissão de armazenamento. Tem certeza de que deseja continuar sem concedê-la?",
                        "Sim",
                        "Não"
                    );

                    if (confirmExit)
                    {
                        // Se o usuário confirmar, fechar o aplicativo
                        FinishAffinity();
                    }
                }
            }
        }

        private Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            var tcs = new TaskCompletionSource<bool>();

            new AlertDialog.Builder(this)
                .SetTitle(title)
                .SetMessage(message)
                .SetPositiveButton(accept, (sender, args) => tcs.SetResult(true))
                .SetNegativeButton(cancel, (sender, args) => tcs.SetResult(false))
                .Show();

            return tcs.Task;
        }
    }

}
