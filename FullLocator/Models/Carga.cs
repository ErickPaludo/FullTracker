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
        private string longitude; 
        [ObservableProperty]
        private string latitude;

        public Carga()
        {
        }

        public Carga(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;          
        }
    }
}
