using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTaurant.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public int FoodId { get; set; }
        public int QuantityChange { get; set; }
        public DateTime ChangeDate { get; set; }

        public Food Food { get; set; }
    }
}
