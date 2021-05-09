using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shopbridge.Data;
using Shopbridge.IRepository;

namespace Shopbridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockProcessController : ControllerBase
    {
        private IStockProcessRepository _repos;
        public StockProcessController(IStockProcessRepository _repository)
        {
            this._repos = _repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllStockItems")]
        public async Task<List<StockInfo>> GetAllStockItems()
        {
            List<StockInfo> response = new List<StockInfo>();
            try
            {
                response = await _repos.GetAllStockItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockrequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStockItem")]
        public async Task<string> AddStockItem(StockInfo stockrequest)
        {
            string response = string.Empty;
            try
            {
                if (stockrequest != null)
                {
                    var req = stockrequest;
                    response = await _repos.AddStockItem(req);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("RemoveStockItem")]
        public async Task<string> RemoveStockItem(int? StockId)
        {
            string response = string.Empty;
            try
            {
                if (StockId != null)
                {
                    var req = StockId;
                    response = await _repos.RemoveStockItem(req);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("UpdateStockItem")]
        public async Task<string> UpdateStockItem(StockInfo stockrequest)
        {
            string response = string.Empty;
            try
            {
                if (stockrequest != null)
                {
                    var req = stockrequest;
                    response = await _repos.UpdateStockItem(req);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
