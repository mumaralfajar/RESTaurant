using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTaurant.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }

        public Customer Customer { get; set; }
    }
}
