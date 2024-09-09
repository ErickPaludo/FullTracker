using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
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
    [SQLite.Table("tb_config")]
    public partial class DataConfig 
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        [SQLite.NotNull]
        public string http_api { get; set; }
        [SQLite.MaxLength(12), SQLite.NotNull]
        public string location_precision { get; set; }
        [SQLite.MaxLength(5), SQLite.NotNull]
        public int time { get; set; }

        public DataConfig()
        {
        }

    }
}
