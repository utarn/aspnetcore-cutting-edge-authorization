using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;

namespace CuttingEdgeAuthorization
{
    public class ApplicationDbContextSeed
    {

        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager)
        {
            var user1 = new IdentityUser()
            {
                UserName = "utarn1@localhost",
                Email = "utarn1@localhost",
                EmailConfirmed = true
            };
            if (!userManager.Users.Any(u => u.UserName == user1.UserName))
            {
                await userManager.CreateAsync(user1, "123456");
            }

            var user1Claims = await userManager.GetClaimsAsync(user1);

            if (!user1Claims.Any(c => c.Type == ClaimTypes.DateOfBirth))
            {
                await userManager.RemoveClaimsAsync(user1, user1Claims.Where(c => c.Type == ClaimTypes.DateOfBirth));
            }
            await userManager.AddClaimAsync(user1, new Claim(ClaimTypes.DateOfBirth,
                        new DateTime(1980, 1, 1).Ticks.ToString()));

            if (!user1Claims.Any(c => c.Type == ClaimTypes.Role))
            {
                await userManager.RemoveClaimsAsync(user1, user1Claims.Where(c => c.Type == ClaimTypes.Role));
            }
            await userManager.AddClaimAsync(user1, new Claim(ClaimTypes.Role,
                        "Administrator"));


            var user2 = new IdentityUser()
            {
                UserName = "utarn2@localhost",
                Email = "utarn2@localhost",
                EmailConfirmed = true
            };
            if (!userManager.Users.Any(u => u.UserName == user2.UserName))
            {
                await userManager.CreateAsync(user2, "123456");
            }

            var user2Claims = await userManager.GetClaimsAsync(user2);

            if (!user2Claims.Any(c => c.Type == ClaimTypes.DateOfBirth))
            {
                await userManager.RemoveClaimsAsync(user2, user2Claims.Where(c => c.Type == ClaimTypes.DateOfBirth));
            }
            await userManager.AddClaimAsync(user2, new Claim(ClaimTypes.DateOfBirth,
                        new DateTime(2000, 1, 1).Ticks.ToString()));

            if (!user2Claims.Any(c => c.Type == ClaimTypes.Role))
            {
                await userManager.RemoveClaimsAsync(user2, user2Claims.Where(c => c.Type == ClaimTypes.Role));
            }
            await userManager.AddClaimAsync(user2, new Claim(ClaimTypes.Role,
                        "User"));

        }
    }
}