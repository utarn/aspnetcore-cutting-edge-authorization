using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CuttingEdgeAuthorization.Data;
using Microsoft.AspNetCore.Authorization;

namespace CuttingEdgeAuthorization
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        private readonly ApplicationDbContext _dbContext;
        public AgeRequirementHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            var user = context.User;
            if (!user.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var birthDate = new DateTime(long.Parse(user.FindFirstValue(ClaimTypes.DateOfBirth)));
            var age = DateTime.Now.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Now.AddYears(-age))
            {
                age--;
            }
            if (age >= requirement.Year)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}