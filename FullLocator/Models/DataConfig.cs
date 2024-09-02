using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullLocator.Models
{
    [Table("tb_config")]
    public partial class DataConfig 
    {
        [MaxLength(8),NotNull]
        public string ncarga { get; set; }

        [NotNull]
        public string http_api { get; set; }

        public int location_precision { get; set; }
        [MaxLength(5), NotNull]
        public int time { get; set; }

        public DataConfig()
        {
        }

    }
}
