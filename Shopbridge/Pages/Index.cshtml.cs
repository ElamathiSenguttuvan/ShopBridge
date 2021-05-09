using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shopbridge.Controllers;
using Shopbridge.Data;
using Shopbridge.IRepository;

namespace Shopbridge.Pages
{
   
    public class IndexModel : PageModel
    {
        private IStockProcessRepository _repos;
        private static ShopBridgeContext _dbcontext;
        public IndexModel(IStockProcessRepository _repository, ShopBridgeContext dbcontext)
        {
            this._repos = _repository;
            _dbcontext = dbcontext;
        }

        public List<StockInfo> StockDetails { get; set; }
        public string Msg { get; set; }

        public async Task OnGetAsync()
        {
            StockProcessController _controller = new StockProcessController(_repos);
            StockDetails = await _controller.GetAllStockItems();
        }
       
    }
}
