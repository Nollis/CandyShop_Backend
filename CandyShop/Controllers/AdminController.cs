using CandyShop.Areas.Identity.Data;
using CandyShop.Models;
using CandyShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace CandyShop.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("users")]
        public List<ApplicationUser> GetUsers()
        {
            return _context.Customers.Include(c => c.Cart).Include(i => i.Cart.ItemOrders).ToList();
            //return _context.Customers.ToList();
        }


        [HttpDelete("deleteuser/{id}")]
        public IActionResult DeleteUser(string id)
        {
            ApplicationUser customer = _context.Customers.Find(id);
            if (customer == null) return StatusCode(400);

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return StatusCode(200);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCandy(int id, [FromBody] Candy updatedCandy)
        {
            if (updatedCandy == null)
                return BadRequest("Candy ID mismatch");

            try
            {
                _context.Candies.Update(updatedCandy);
                _context.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data: " + ex);
            }
        }

        [HttpPost("create")]
        public IActionResult Create(JsonObject candy)
        {
            string jsonCandy = candy.ToString();
            Candy candyToCreate = JsonConvert.DeserializeObject<Candy>(jsonCandy);

            if (candyToCreate != null)
            {
                _context.Candies.Add(candyToCreate);
                _context.SaveChanges();

                return StatusCode(200);
            }

            return StatusCode(404);
        }

        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser(UpdateUserVM vm)
        {
           var customer = await _context.Customers.FindAsync(vm.UserId);

            customer.CustomerFName = vm.CustomerFName;
            customer.CustomerLName = vm.CustomerLName;
            customer.PhoneNumber = vm.PhoneNumber;


            await _userManager.UpdateAsync(customer);
           
            return StatusCode(200);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var candy = _context.Candies.Find(id);

            if (candy != null)
            {
                _context.Candies.Remove(candy);
                _context.SaveChanges();

                return StatusCode(200);
            }
            return StatusCode(404);
        }

        [HttpGet("itemorders")]
        public List<ItemOrder> GetItemOrders()
        {
            return _context.ItemOrders.ToList();
        }

        [HttpDelete("deleteorders/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var ItemOrder = _context.ItemOrders.Find(id);

            if (ItemOrder != null)
            {
                _context.ItemOrders.Remove(ItemOrder);
                _context.SaveChanges();

                return StatusCode(200);
            }
            return StatusCode(404);
        }

        [HttpPut("updateorders/{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] ItemOrder updatedOrder)
        {
            if (updatedOrder == null)
                return BadRequest("Candy ID mismatch");

            try
            {
                _context.ItemOrders.Update(updatedOrder);
                _context.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data: " + ex);
            }
        }

    }
}
