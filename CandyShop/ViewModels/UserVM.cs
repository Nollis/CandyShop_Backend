using CandyShop.Areas.Identity.Data;

namespace CandyShop.ViewModels
{
    public class UserVM
    {
        public string UserId { get; set; }

        public string CustomerFName { get; set; }

        public string CustomerLName { get; set; }

        public string Email { get; set; }

        public int CartId { get; set; }

        public bool IsAdmin { get; set; }


    }
}
