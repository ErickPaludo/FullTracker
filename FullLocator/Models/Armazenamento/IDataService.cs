using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullLocator.Models.Armazenamento
{
    public interface IDataService
    {
        Task InitilizeAsync();
        Task<List<DataConfig>> GetDataConfigAsync();
        Task<DataConfig> GetData(int carga);
        Task<int> AddData(DataConfig data);
        Task<int> UpdateData(DataConfig data);
        Task<int> DeleteData(DataConfig data);
    }
}
