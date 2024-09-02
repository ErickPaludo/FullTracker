using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullLocator.Models
{
    public partial class Carga : ObservableObject
    {
        [ObservableProperty]
        private string ncarga;
        [ObservableProperty]
        private string longitude; 
        [ObservableProperty]
        private string latitude;

        public Carga()
        {
        }

        public Carga(string ncarga, string latitude, string longitude)
        {
            Ncarga = ncarga;
            Latitude = latitude;
            Longitude = longitude;          
        }

        public Carga(string longitude, string latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
