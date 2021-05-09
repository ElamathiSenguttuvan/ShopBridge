
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Shopbridge.Controllers;
using Shopbridge.Data;
using Shopbridge.IRepository;
using Shopbridge.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Shopbridge.UnitTest
{
    public class StockProcessUnitTest
    {
        //private static IStockProcessRepository _repos;

        //private ShopBridgeContext dbcontext;
        private IConfiguration _config;
        private static DbContextOptionsBuilder<ShopBridgeContext> optionsBuilder;
        private static ShopBridgeContext dbctxt;
        public StockProcessUnitTest()
        {
            optionsBuilder = new DbContextOptionsBuilder<ShopBridgeContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ShopBridgeDB"));
            dbctxt = new ShopBridgeContext(optionsBuilder.Options);
        }
        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile($"TestSettings.json", optional: false);
                    _config = builder.Build();
                }
                return _config;
            }

        }
        public static void Main()
        {

        }
        [Fact]
        public async Task GetAllStockItems_ValidRequest()
        {
            StockProcessRepository repo = new StockProcessRepository(dbctxt);
            var response = await repo.GetAllStockItems();
            Assert.NotNull(response);
        }
        [Fact]
        public void AddStockItem_ValidRequest()
        {
            StockProcessRepository repo = new StockProcessRepository(dbctxt);

            StockInfo info = new StockInfo();
            info.StockId = 11;
            info.Name = "JBL GO2 Speaker";
            info.Price = 2166.00M;
            info.ProductType = "Electronics";
            info.Rating = 4;
            info.AvailabilityStatus = "Yes";
            string result = repo.AddStockItem(info).Result;
            Assert.Equal(result, GlobalConstants.Success);
        }
        [Fact]
        public void AddStockItem_InValidRequest_DuplicateStockId()
        {
            StockProcessRepository repo = new StockProcessRepository(dbctxt);

            StockInfo info = new StockInfo();
            info.StockId = 3;
            info.Name = "JBL GO2 Speaker";
            info.Price = 2166.00M;
            info.ProductType = "Electronics";
            info.Rating = 4;
            info.AvailabilityStatus = "Yes";
            string result = repo.AddStockItem(info).Result;
            Assert.Equal("Trying to add Duplicate StocckId.Kindly recheck the Id to be updated", result);
        }
        [Fact]
        public void RemoveStockItem_ValidRequest()
        {
            StockProcessRepository repo = new StockProcessRepository(dbctxt);

            int id =4;
            string result = repo.RemoveStockItem(id).Result;
            Assert.Equal(result, GlobalConstants.Success);
        }
        [Fact]
        public void RemoveStockItem_InValidRequest_InvalidStockId()
        {
            StockProcessRepository repo = new StockProcessRepository(dbctxt);

            int id = 20;
            string result = repo.RemoveStockItem(id).Result;
            Assert.Equal("Not a valid StockId", result);
        }
        [Fact]
        public void UpdateStockItem_ValidRequest()
        {
            StockProcessRepository repo = new StockProcessRepository(dbctxt);

            StockInfo info = new StockInfo();
            info.StockId = 11;
            info.Name = "JBL GO2 Speaker";
            info.Price = 2166.00M;
            info.ProductType = "Electronics";
            info.Rating = 3.3;
            info.AvailabilityStatus = "Yes";
            string result = repo.UpdateStockItem(info).Result;
            Assert.Equal(result, GlobalConstants.Success);
        }
        [Fact]
        public void UpdateStockItem_InValidRequest()
        {
            StockProcessRepository repo = new StockProcessRepository(dbctxt);

            StockInfo info = new StockInfo();
            info.StockId = 11;
            info.Name = "JBL GO2 Speaker";
            info.Price = 2166.00M;
            info.ProductType = "Electronics";
            info.Rating = 3.2;
            info.AvailabilityStatus = "Yes";
            string result = repo.UpdateStockItem(info).Result;
            Assert.Equal(result, GlobalConstants.Failure);
        }
    }
}
