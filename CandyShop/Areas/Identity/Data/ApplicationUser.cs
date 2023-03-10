using CandyShop.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CandyShop.Areas.Identity.Data;

public class ApplicationUser : IdentityUser
{
    public string CustomerFName { get; set; }

    public string CustomerLName { get; set; }

    public string PhoneNumber { get; set; }

    public List<Order> Orders = new List<Order>();

    public Cart Cart { get; set; }


}



