using CandyShop.Models;

namespace CandyShop.ViewModels
{
    public class CandiesAndCategoriesVM
    {
        public List<Candy> Candies { get; set; } = new List<Candy>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
