using FullLocator.Models.Armazenamento;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullLocator.Models
{
    public class DataService : IDataService
    {
        private SQLiteAsyncConnection _dbConnection;

        public async Task InitilizeAsync()
        {
            await SetUpDb();
        }
        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3");
                _dbConnection = new SQLiteAsyncConnection(dpPath);
                await _dbConnection.CreateTableAsync<DataConfig>();
            }
        }

        public async Task<int> AddData(DataConfig data)
        {
           return await _dbConnection.InsertAsync(data);
        }

        public async Task<int> DeleteData(DataConfig data)
        {
            return await _dbConnection.DeleteAsync(data);
        }

        public async Task<int> UpdateData(DataConfig data)
        {
            if (data.id == 1)
            {
                return await _dbConnection.UpdateAsync(data);
            }
            else
            {
                throw new ArgumentException("");
            }
        }

        public async Task<List<DataConfig>> GetDataConfigAsync()
        {
            return await _dbConnection.Table<DataConfig>().ToListAsync();
        }
        public async Task<DataConfig> GetData(int id)
        {
            return await _dbConnection.Table<DataConfig>().FirstOrDefaultAsync(x => x.id == id);
        }

      
    }
}
