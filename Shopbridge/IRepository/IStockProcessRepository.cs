using Shopbridge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge.IRepository
{
    public interface IStockProcessRepository
    {
        public Task<List<StockInfo>> GetAllStockItems();
        public  Task<string> AddStockItem(StockInfo stockrequest);
        public  Task<string> RemoveStockItem(int? StockId);
        public  Task<string> UpdateStockItem(StockInfo stockrequest);
    }
}
