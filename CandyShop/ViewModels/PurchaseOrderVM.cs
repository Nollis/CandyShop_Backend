using CandyShop.Models;

namespace CandyShop.ViewModels
{
    public class PurchaseOrderVM
    {
        public List<ItemOrder> ItemOrders { get; set; }
        public int TotalAmount { get; set; }
        public string UserId { get; set; }
    }
}
