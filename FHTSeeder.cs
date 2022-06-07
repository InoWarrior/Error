using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFHT.Entities;

namespace WebApiFHT
{


    public class FHTSeeder
    {
        private readonly FHTDbContext _dbContext;

        public FHTSeeder(FHTDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Company.Any())
                {
                    var Companies = GetCompanies();
                    _dbContext.Company.AddRange(Companies);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"

                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }
        private IEnumerable<Company> GetCompanies()
        {
            var Companies = new List<Company>() {

                new Company()
                {
                    Name = "Nutrena",
                    Category = "Agricultural",
                    Description = "Nutrena is a complete feed manufacturer for cattle, cows and chickens",
                    ContactEmail = "contact@nutrena.com",
                    HasDelivery = true,
                    ContactNumber = "789456123",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            ProductName = "Kromilk 38",
                            Description = "Complete feed",
                            Price = 64,
                            IsAvailable = true,

                        },
                        new Product()
                        {
                            ProductName = "Kromilk Białe",
                            Description = "Milk",
                            Price = 130,
                            IsAvailable = true,
                        },
                        new Product()
                        {
                            ProductName = "Kromilk Len",
                            Description = "Milk",
                            Price = 130,
                            IsAvailable = true,
                        }
                    },
                    Address = new Address()
                    {
                        City = "Warsaw",
                        Street = "Wiejska 47",
                        PostalCode = "58-456",

                    }


                },
                new Company()
                {
                    Name = "Josera",
                    Category = "Agricultural",
                    Description = "Josera Helping grow ",
                    ContactEmail = "contact@josera.com",
                    HasDelivery = true,
                    ContactNumber = "789456123",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            ProductName = "Lacto Plus Extra",
                            Description = "Vitamins",
                            Price = 130,
                            IsAvailable = true,

                        },
                        new Product()
                        {
                            ProductName = "Fe-Trank",
                            Description = "Milk",
                            Price = 150,
                            IsAvailable = true,
                        },
                        new Product()
                        {
                            ProductName = "Supramil",
                            Description = "Milk",
                            Price = 180,
                            IsAvailable = true,
                        }
                    },
                    Address = new Address()
                    {
                        City = "Cracow",
                        Street = "Gdańska 47",
                        PostalCode = "12-456",

                    }

                }
            };
            return Companies;
            
        }
    }
}
