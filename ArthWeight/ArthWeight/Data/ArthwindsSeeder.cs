using ArthWeight.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace ArthWeight.Data
{
    public class ArthwindsSeeder
    {
        private readonly ArthwindsContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<User> _userManager;

        public ArthwindsSeeder(ArthwindsContext ctx,
          IHostingEnvironment hosting,
          UserManager<User> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("arth@arthwinds.com");

            if (user == null)
            {
                user = new User()
                {
                    FirstName = "Arthur",
                    LastName = "Gorr",
                    UserName = "arth@arthwinds.com",
                    Email = "arth@arthwinds.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }
        }
    }
}
