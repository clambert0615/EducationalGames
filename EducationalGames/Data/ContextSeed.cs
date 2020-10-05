using EducationalGames.Models;
using EnumsNET;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalGames.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Teacher.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Parent.ToString()));
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultAdmin = new ApplicationUser
            {
                UserName = "KJones",
                Email = "kjones@educationalgames.com",
                FirstName = "Katelyn",
                LastName = "Jones",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
               
            };
            if(userManager.Users.All(u => u.Id != defaultAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if(user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "Hello123!");
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Teacher.ToString());
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Student.ToString());
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Parent.ToString());
                }
            }
        }
    }
}
