using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Shopbridge.Data
{
    public partial class StockInfo
    {
        [Key]
        public int StockId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ProductType { get; set; }
        public string AvailabilityStatus { get; set; }
        public double? Rating { get; set; }
    }
}
