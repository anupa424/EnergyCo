using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShopping.Models
{
    [NotMapped]
    public class ShoppingRequest
    {
        public Guid CustomerId { get; set; }
        public string LoyaltyCard { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<Basket> Basket { get; set; }
    }
}
