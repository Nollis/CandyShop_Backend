using CandyShop.Areas.Identity.Data;
using CandyShop.Models;
using CandyShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Diagnostics.Metrics;

namespace CandyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public CandyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserStore<ApplicationUser> userStore)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();

                user.CustomerFName = model.CustomerFName;
                user.CustomerLName = model.CustomerLName;
                user.PhoneNumber = model.PhoneNumber;


                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    Cart cart = new Cart { CustomerCartId = user.Id};
                    _context.Carts.Add(cart);
                    _context.SaveChanges();
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return StatusCode(200);
                }
            }

            return StatusCode(400);
        }


        [HttpGet]
        public List<Candy> GetCandies()
        {
            return _context.Candies.ToList();
        }


        [HttpGet("categories")]
        public List<Category> GetCategories()
        {
            return _context.Categories.Include(x => x.Candies).ToList();
        }

        [HttpDelete("categories/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();

                return StatusCode(200);
            }
            return StatusCode(404);
        }

        [HttpPut("categories/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest("Category ID mismatch");

            try
            {
                _context.Categories.Update(updatedCategory);
                _context.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data: " + ex);
            }
        }

        [HttpPost("createCategory")]
        public IActionResult CreateCategory(JsonObject category)
        {
            string jsonCategory = category.ToString();
            Category categoryToCreate = JsonConvert.DeserializeObject<Category>(jsonCategory);

            if (categoryToCreate != null)
            {
                _context.Categories.Add(categoryToCreate);
                _context.SaveChanges();

                return StatusCode(200);
            }

            return StatusCode(404);
        }


        [HttpPost("add")]
        public IActionResult AddToCart(JsonObject item)
        {
            string jsonItem = item.ToString();

            ItemOrder itemObject = JsonConvert.DeserializeObject<ItemOrder>(jsonItem);

            if (itemObject != null)
            {
                _context.ItemOrders.Add(itemObject);
                _context.SaveChanges();

                return StatusCode(200);
            }
            return StatusCode(400);


        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return StatusCode(200);
        }

        [HttpPost("login")]
        public async Task<UserVM> Login(LoginVM model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = _context.Customers.Include(c => c.Cart).FirstOrDefault(x => x.Email == model.Email);
                UserVM userVM = new UserVM { CustomerFName = user.CustomerFName, CustomerLName = user.CustomerLName, Email = user.Email, UserId = user.Id, CartId = user.Cart.CartId, IsAdmin = false};
                if (User.IsInRole("Admin")) { userVM.IsAdmin = true; }

                return userVM;
            }
            return null;
        }

        [HttpGet("getusername")]
        public string GetUsername()
        {
            var username = _userManager.GetUserName(User);
            if(username == null || username == "") { return "null"; }
            return username;
        }

        [HttpGet("getroles")]
        public async Task<List<string>> GetRoles()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }


        [HttpGet("checkadmin")]
        public bool CheckIfAdmin()
        {
            return User.IsInRole("Admin");
        }


        [HttpGet("getid")]
        public async Task<string> GetId()
        {
            var user = await _userManager.GetUserAsync(User);
        

            return user.Id;
        }

        //[HttpGet("purchase/{id}")]
        //public Order Purchase(string id)
        //{
        //    int totalAmount = 0;

        //    Cart cart = _context.Carts.Include(i => i.ItemOrders).FirstOrDefault(x => x.CustomerCartId == id);

        //    foreach (var item in cart.ItemOrders)
        //    {
        //        Candy candy = _context.Candies.Find(item.CandyId);
        //        candy.CandyQuantity -= item.Quantity;
        //        _context.Candies.Update(candy);
        //        totalAmount += item.CandyPrice * item.Quantity;
        //    }

        //    Order order = new Order { Items = cart.ItemOrders, OrderDate = DateTime.Today, TotalAmount = totalAmount, UserId = id};
        //    _context.Orders.Add(order);


        //    _context.SaveChanges();

        //    return order;
        //}


        [HttpPost("purchase")]
        public Order Purchase([FromBody] JsonObject jsonObject)
        {

            string jsonItem = jsonObject.ToString();
            PurchaseOrderVM vm = JsonConvert.DeserializeObject<PurchaseOrderVM>(jsonItem);

            Order order = new Order { Items = vm.ItemOrders, OrderDate = DateTime.Today, TotalAmount = vm.TotalAmount, UserId = vm.UserId };
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }



        [HttpGet("onlyCategories")]
        public List<Category> GetonlyCategories()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("showhistory/{id}")]
        public List<Order> ShowHistory(string id)
        {
           var customer = _context.Customers.Include(o => o.Orders).FirstOrDefault(x => x.Id == id);

            return customer.Orders.ToList();
        }

        [HttpGet("orders")]
        public List<ItemOrder> ShowOrders()
        {
            return _context.ItemOrders.ToList();
        }


        [HttpGet("showcart/{id}")]
        public Cart ShowCart(string id)
        {
            return _context.Carts.Include(i => i.ItemOrders).FirstOrDefault(x => x.CustomerCartId == id);
        }


        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
