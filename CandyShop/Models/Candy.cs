using System.ComponentModel.DataAnnotations;

namespace CandyShop.Models
{
    public class Candy
    {
        [Key]
        public int CandyId { get; set; }

        public string? CandyName { get; set; }

        public string? CandyDescription { get; set; }

        public Category? Category { get; set; }

        public int CandyCategoryId { get; set; }

        public double CandyPrice { get; set; }

        public int CandyQuantity { get; set; }

        public string? CandyImage { get; set; }

        //public string Image { get; set; }

    }
}

