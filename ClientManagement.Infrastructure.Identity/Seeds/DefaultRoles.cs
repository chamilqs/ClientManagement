﻿using ClientManagement.Core.Application.Enums;
using ClientManagement.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClientManagement.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        }
    }
}
