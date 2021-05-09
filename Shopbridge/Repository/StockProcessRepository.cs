using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shopbridge.Data;
using Shopbridge.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge.Repository
{
    public class StockProcessRepository: IStockProcessRepository
    {
        private ShopBridgeContext dbcontext;

        public StockProcessRepository(ShopBridgeContext dbcontext)
        {
            this.dbcontext = dbcontext;         
        }

        public async Task<List<StockInfo>> GetAllStockItems()
        {
            List<StockInfo> info = new List<StockInfo>();
            try
            {
                info = (from test in dbcontext.StockInfos.AsNoTracking()
                        select new StockInfo
                        {
                            StockId = test.StockId,
                            Name = test.Name,
                            Description=test.Description,
                            ProductType=test.ProductType,
                            Price=test.Price,
                            AvailabilityStatus=test.AvailabilityStatus,
                            Rating=test.Rating
                        }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(info);

        }
        public async Task<string> AddStockItem(StockInfo stockrequest)
        {
            string response = string.Empty;
            try
            {
                dbcontext.Add(stockrequest);
                var val = dbcontext.SaveChanges();
                if (val == 1)
                    response = GlobalConstants.Success;
                else
                    response = GlobalConstants.Failure;
            }
            catch(Exception ex)
            {
                if (ex.InnerException.Message.Contains("PRIMARY KEY"))
                     return response = "Trying to add Duplicate StocckId.Kindly recheck the Id to be updated";
                else
                    return response = ex.InnerException.Message;
                
            }
                return await Task.FromResult(response);
        }
        public async Task<string> RemoveStockItem(int? StockId)
        {
            string response = string.Empty;
            try
            {
                var item = dbcontext.StockInfos.AsNoTracking().Where(x => x.StockId == StockId).FirstOrDefault();
                if(item != null)
                {
                    dbcontext.Remove(item);
                    var val = dbcontext.SaveChanges();
                    if (val == 1)
                        response = GlobalConstants.Success;
                    else
                        response = GlobalConstants.Failure;
                }
                else
                {
                    return "Not a valid StockId";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(response);
        }
            public async Task<string> UpdateStockItem(StockInfo stockrequest)
        {
            string response = string.Empty;
            try
            {
                var item = dbcontext.StockInfos.Where(x => x.StockId == stockrequest.StockId).FirstOrDefault();
                if (item != null)
                {
                    if (!String.IsNullOrEmpty(stockrequest.Name))
                        item.Name = stockrequest.Name;
                    if (!String.IsNullOrEmpty(stockrequest.Description))
                        item.Description = stockrequest.Description;
                    if (stockrequest.Price != null)
                        item.Price = stockrequest.Price;
                    if (String.IsNullOrEmpty(stockrequest.ProductType))
                        item.ProductType = stockrequest.ProductType;
                    if (String.IsNullOrEmpty(stockrequest.AvailabilityStatus))
                        item.AvailabilityStatus = stockrequest.AvailabilityStatus;
                    if (stockrequest.Rating != null)
                        item.Rating = stockrequest.Rating;
                    var val = dbcontext.SaveChanges();
                    if (val == 1)
                        response = GlobalConstants.Success;
                    else
                        response = GlobalConstants.Failure;
                }
                else
                {
                    response = "Invalid Stock Id";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(response);
        }

    }
}
