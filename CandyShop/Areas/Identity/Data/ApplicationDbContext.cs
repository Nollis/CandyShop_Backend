using CandyShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace CandyShop.Areas.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public DbSet<ApplicationUser> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Candy> Candies { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            string adminRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();

            string customerId = Guid.NewGuid().ToString();
            string customerId2 = Guid.NewGuid().ToString();

           // modelBuilder.Entity<ApplicationUser>().HasKey(p => p.CustomerId);
           modelBuilder.Entity<Cart>().HasKey(c => c.CartId);

            modelBuilder.Entity<ApplicationUser>().HasOne(c => c.Cart).WithOne(u => u.Customer).HasForeignKey<Cart>(c => c.CustomerCartId);
            modelBuilder.Entity<ItemOrder>().HasOne(c => c.Cart).WithMany(u => u.ItemOrders).HasForeignKey(c => c.CartId);
            modelBuilder.Entity<Order>().HasOne(a => a.ApplicationUser).WithMany(o => o.Orders).HasForeignKey(u => u.UserId);
            modelBuilder.Entity<Candy>().HasOne(c => c.Category).WithMany(c => c.Candies).HasForeignKey(x => x.CandyCategoryId);


            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
            {
                Id = customerId,
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL:COM",
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Abc123!"),
                CustomerFName = "Admin",
                CustomerLName = "Adminsson",
                PhoneNumber = "0737555555",
            },

                new ApplicationUser
            {
                Id = customerId2,
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL:COM",
                UserName = "user@gmail.com",
                NormalizedUserName = "user@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Abc123!"),
                CustomerFName = "User",
                CustomerLName = "Usersson",
                PhoneNumber = "0737555555",
                }
            );

            modelBuilder.Entity<Candy>().HasData(
               new Candy
               {
                   CandyId = 1,
                   CandyName = "Coffee Rio",
                   CandyCategoryId = 1,
                   CandyDescription = "Caramels are made by cooking sugar and water together",
                   CandyPrice = 5,
                   CandyQuantity = 50,
                   CandyImage = "Choklad1.jpg"
               },
                new Candy
                {
                    CandyId = 2,
                    CandyName = "Caramel Crunch",
                    CandyCategoryId = 1,
                    CandyDescription = "A tasty Caramel and sugary Crunch",
                    CandyPrice = 15,
                    CandyQuantity = 50,
                    CandyImage = "Choklad2.png"
                },
                new Candy
                {
                    CandyId = 3,
                    CandyName = "Vanilla Caramel",
                    CandyCategoryId = 1,
                    CandyDescription = "The Vanila taste with the sweet of caramel",
                    CandyPrice = 10,
                    CandyQuantity = 50,
                    CandyImage = "Choklad3.jpg"
                },
                new Candy
                {
                    CandyId = 4,
                    CandyName = "Caramel Squares",
                    CandyCategoryId = 1,
                    CandyDescription = "The Vanilla taste with the sweet of caramel",
                    CandyPrice = 10,
                    CandyQuantity = 50,
                    CandyImage = "Choklad4.jpg"
                },
                new Candy
                {
                    CandyId = 5,
                    CandyName = "Nut Caramel",
                    CandyCategoryId = 1,
                    CandyDescription = "Combination of nuts and the sweet of caramel",
                    CandyPrice = 15,
                    CandyQuantity = 40,
                    CandyImage = "Choklad5.jpg"
                },
                new Candy
                {
                    CandyId = 6,
                    CandyName = "M&Ms",
                    CandyCategoryId = 2,
                    CandyDescription = "Tastey colorful chocolate",
                    CandyPrice = 15,
                    CandyQuantity = 40,
                    CandyImage = "Gele1.jpg"
                },
                 new Candy
                 {
                     CandyId = 7,
                     CandyName = "Kit Kat",
                     CandyCategoryId = 2,
                     CandyDescription = "A mini full of flavor chocolate",
                     CandyPrice = 11,
                     CandyQuantity = 45,
                     CandyImage = "Gele2.jpg"
                 },
                new Candy
                {
                    CandyId = 8,
                    CandyName = "Almond Joy",
                    CandyCategoryId = 2,
                    CandyDescription = "Combination of Almond and the sweet of chocolate",
                    CandyPrice = 16,
                    CandyQuantity = 40,
                    CandyImage = "Gele3.jpg"
                },
                new Candy
                {
                    CandyId = 9,
                    CandyName = "Chocolate Cherries",
                    CandyCategoryId = 2,
                    CandyDescription = "Combination of Cherries flavor and the sweet of chocolate",
                    CandyPrice = 15,
                    CandyQuantity = 50,
                    CandyImage = "Gele4.jpg"
                },
                new Candy
                {
                    CandyId = 10,
                    CandyName = "Krackle",
                    CandyCategoryId = 2,
                    CandyDescription = "Biscute Coverd of chocolate",
                    CandyPrice = 11,
                    CandyQuantity = 50,
                    CandyImage = "Gele5.jpg"
                },
                new Candy
                {
                    CandyId = 11,
                    CandyName = "Snickers",
                    CandyCategoryId = 2,
                    CandyDescription = "Combination of nuts and the sweet of chocolate",
                    CandyPrice = 15,
                    CandyQuantity = 40,
                    CandyImage = "Gele2.jpg"
                },
                new Candy
                {
                    CandyId = 12,
                    CandyName = "Gummi Cheries",
                    CandyCategoryId = 3,
                    CandyDescription = "Gummies are gelatin based chewy candies",
                    CandyPrice = 18,
                    CandyQuantity = 55,
                    CandyImage = "Karamell1.jpg"
                },
                 new Candy
                 {
                     CandyId = 13,
                     CandyName = "Gummi worms",
                     CandyCategoryId = 3,
                     CandyDescription = "Chewy candies With different flavors",
                     CandyPrice = 5,
                     CandyQuantity = 60,
                     CandyImage = "Karamell2.jpg"
                 },

                 new Candy
                 {
                     CandyId = 14,
                     CandyName = "Gummi cola bottles",
                     CandyCategoryId = 3,
                     CandyDescription = "Chewy candies With cola flavor",
                     CandyPrice = 5,
                     CandyQuantity = 60,
                     CandyImage = "Karamell3.jpg"
                 },
                new Candy
                {
                    CandyId = 15,
                    CandyName = "Gummi strawberry",
                    CandyCategoryId = 3,
                    CandyDescription = "Chewy candies With stawberry flavor",
                    CandyPrice = 5,
                    CandyQuantity = 60,
                    CandyImage = "Karamell4.jpg"
                },
                new Candy
                {
                    CandyId = 16,
                    CandyName = "Gummi banana",
                    CandyCategoryId = 3,
                    CandyDescription = "Chewy candies With banana flavor",
                    CandyPrice = 5,
                    CandyQuantity = 60,
                    CandyImage = "Karamell5.jpg"
                },
                new Candy
                {
                    CandyId = 17,
                    CandyName = "Red Vines",
                    CandyCategoryId = 4,
                    CandyDescription = "Licorice is a semi-soft candy",
                    CandyPrice = 20,
                    CandyQuantity = 50,
                    CandyImage = "Klubba1.jpg"
                },
                 new Candy
                 {
                     CandyId = 18,
                     CandyName = "Twizzler",
                     CandyCategoryId = 4,
                     CandyDescription = "Licorice is a semi-soft candy with cherry flavor",
                     CandyPrice = 20,
                     CandyQuantity = 50,
                     CandyImage = "Klubba2.jpg"
                 },
                  new Candy
                  {
                      CandyId = 19,
                      CandyName = "Chupa Chups",
                      CandyCategoryId = 4,
                      CandyDescription = "A hard sweet candy",
                      CandyPrice = 15,
                      CandyQuantity = 40,
                      CandyImage = "Klubba3.jpg"
                  },
                   new Candy
                   {
                       CandyId = 20,
                       CandyName = "Sour Punch",
                       CandyCategoryId = 4,
                       CandyDescription = "A sour candy",
                       CandyPrice = 13,
                       CandyQuantity = 30,
                       CandyImage = "Klubba4.jpg"
                   },
                  new Candy
                  {
                      CandyId = 21,
                      CandyName = "Warheads",
                      CandyCategoryId = 4,
                      CandyDescription = "A sour candy",
                      CandyPrice = 15,
                      CandyQuantity = 40,
                      CandyImage = "Klubba5.jpg"
                  },
                   new Candy
                   {
                       CandyId = 22,
                       CandyName = "Abba-Zaba",
                       CandyCategoryId = 4,
                       CandyDescription = "Chewy sweet candy",
                       CandyPrice = 10,
                       CandyQuantity = 70,
                       CandyImage = "Klubba6.jpg"
                   },
                    new Candy
                    {
                        CandyId = 23,
                        CandyName = "Sky Bar",
                        CandyCategoryId = 4,
                        CandyDescription = "Chewy sweet candy",
                        CandyPrice = 5,
                        CandyQuantity = 60,
                        CandyImage = "Klubba7.jpg"
                    },
                     new Candy
                     {
                         CandyId = 24,
                         CandyName = "Peach Blossoms",
                         CandyCategoryId = 4,
                         CandyDescription = "Chewy with peach flavor candy",
                         CandyPrice = 10,
                         CandyQuantity = 50,
                         CandyImage = "Klubba8.jpg"
                     }

                );


            modelBuilder.Entity<Category>().HasData(

                 new Category
                 {
                     CategoryId = 1,
                     CategoryName = "Chocolate",
                     CategoryImage = "category1"
                 },

                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Gummies",
                    CategoryImage = "category2"
                },

                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Hard Candy",
                    CategoryImage = "category3"
                },

                 new Category
                 {
                     CategoryId = 4,
                     CategoryName = "Lollipops",
                     CategoryImage = "category4"
                 }

                );

            modelBuilder.Entity<Cart>().HasData(
                new Cart { CartId = 1, CustomerCartId = customerId},
                new Cart { CartId = 2, CustomerCartId = customerId2}
                );




            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string> { RoleId = adminRoleId, UserId = customerId },
               new IdentityUserRole<string> { RoleId = userRoleId, UserId = customerId2 }
               );


        }

    }
}