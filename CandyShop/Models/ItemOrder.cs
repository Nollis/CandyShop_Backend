using System.ComponentModel.DataAnnotations;

namespace CandyShop.Models
{
    public class ItemOrder
    {
        [Key]
        public int ItemOrderId { get; set; }
        public int CandyId { get; set; }
        public string CandyName { get; set; }
        public int CandyPrice { get; set; }
        public int Quantity { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
