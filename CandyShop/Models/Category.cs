using System.ComponentModel.DataAnnotations;

namespace CandyShop.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? CategoryImage { get; set; }

        public List<Candy> Candies { get; set; } = new List<Candy>();
    }
}
