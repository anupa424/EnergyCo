using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShopping.Models
{
    [NotMapped]
    public class Basket
    {
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
