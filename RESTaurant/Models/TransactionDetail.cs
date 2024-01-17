using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTaurant.Models
{
    public class TransactionDetail
    {
        public int TransactionDetailId { get; set; }
        public int TransactionId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Transaction Transaction { get; set; }
        public Food Food { get; set; }
    }
}
