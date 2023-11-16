using FIRPLAKV4.Data.Entities;
using FIRPLAKV4.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace FIRPLAKV4.Data
{
    public class SeedDb
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        public SeedDb(IUserHelper userHelper, DataContext context)
        {
            _userHelper = userHelper;
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await CheckRolesAsync();
            await CheckUsers();

            await CheckOrderStates();

            await CheckProductTypes();

            await CheckProducts();

            await CheckConveyors();
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Auxiliar");
            await _userHelper.CheckRoleAsync("Transportador");
        }

        private async Task CheckUsers()
        {
            // Admin
            User? user = await _userHelper.GetUserAsync("wilder@correo.com");

            if (user == null)
            {
                user = new User
                {
                    Email = "wilder@correo.com",
                    FirstName = "Wilder",
                    LastName = "Durán",
                    PhoneNumber = "321446987",
                    UserName = "wilder@correo.com",
                    Document = "114458784",
                };

                await _userHelper.AddUserAsync(user, "Wilder_123456");
                await _userHelper.AddUserToRoleAsync(user, "Admin");

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            // Auxiliar
            user = await _userHelper.GetUserAsync("ana@correo.com");

            if (user == null)
            {
                user = new User
                {
                    Email = "ana@correo.com",
                    FirstName = "Ana",
                    LastName = "Dow",
                    PhoneNumber = "316474587",
                    UserName = "ana@correo.com",
                    Document = "4587971",
                };

                await _userHelper.AddUserAsync(user, "Ana_123456");
                await _userHelper.AddUserToRoleAsync(user, "Auxiliar");

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            // Transportador
            user = await _userHelper.GetUserAsync("jhon@correo.com");

            if (user == null)
            {
                user = new User
                {
                    Email = "jhon@correo.com",
                    FirstName = "Jhon",
                    LastName = "Doe",
                    PhoneNumber = "302547854",
                    UserName = "jhon@correo.com",
                    Document = "4587971",
                };

                await _userHelper.AddUserAsync(user, "Jhon_123456");
                await _userHelper.AddUserToRoleAsync(user, "Transportador");

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

        }

        private async Task CheckOrderStates()
        {
            if (!await _context.OrderStates.AnyAsync())
            {
                List<OrderState> orderStates = new List<OrderState>()
                {
                    new OrderState
                    {
                        Name = "Activo"
                    },

                    new OrderState
                    {
                        Name = "En reparto"
                    },

                    new OrderState
                    {
                        Name = "Despachado"
                    },

                    new OrderState
                    {
                        Name = "Recibido"
                    },
                };

                await _context.OrderStates.AddRangeAsync(orderStates);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProductTypes()
        {
            if (!await _context.ProductTypes.AnyAsync())
            {
                List<ProductType> productTypes = new List<ProductType>()
                {
                    new ProductType
                    {
                        Name = "MTS"
                    },

                    new ProductType
                    {
                        Name = "MTO"
                    },
                };

                await _context.ProductTypes.AddRangeAsync(productTypes);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProducts()
        {
            if (!await _context.Products.AnyAsync())
            {
                ProductType mts = await _context.ProductTypes.Where(t => t.Name == "MTS").FirstOrDefaultAsync();
                ProductType mto = await _context.ProductTypes.Where(t => t.Name == "MTO").FirstOrDefaultAsync();

                List<Product> products = new List<Product>()
                {
                    new Product
                    {
                        Name = "Bañera con hidromasage",
                        ProductType = mts,
                    },

                    new Product
                    {
                        Name = "Secador de cabello",
                        ProductType = mto,
                    },
                };

                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckConveyors()
        {
            if (!await _context.Conveyors.AnyAsync())
            {
                List<Conveyor> conveyors = new List<Conveyor>()
                {
                    new Conveyor
                    {
                        Name = "Servientrega"
                    },

                    new Conveyor
                    {
                        Name = "Interparidisimo"
                    },

                    new Conveyor
                    {
                        Name = "Envía"
                    },
                };

                await _context.Conveyors.AddRangeAsync(conveyors);
                await _context.SaveChangesAsync();
            }
        }
    }
}
