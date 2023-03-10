using CandyShop.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CandyShop.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public string? CustomerCartId { get; set; }

        public ApplicationUser Customer { get; set; }

        public List<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
    }
}
