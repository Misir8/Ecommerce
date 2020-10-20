using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Misir Askerov",
                    Email = "misirua@code.edu.az",
                    UserName = "misirua@code.edu.az",
                    Address = new Address
                    {
                        Firstname = "Misir",
                        Lastname = "Askerov",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                };

               var result = await userManager.CreateAsync(user, "Admin@12345");
               if (result.Succeeded)
               {
                   await userManager.AddToRoleAsync(user, "Admin");
               }
            }
        }
    }
}
