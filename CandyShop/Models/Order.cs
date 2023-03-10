using CandyShop.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CandyShop.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public List<ItemOrder> Items { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
