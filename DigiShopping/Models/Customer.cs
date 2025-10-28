using System;
using System.ComponentModel.DataAnnotations;

namespace DigiShopping.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoyaltyCard { get; set; }
    }
}
