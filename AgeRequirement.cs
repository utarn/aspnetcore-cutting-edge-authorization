using Microsoft.AspNetCore.Authorization;

namespace CuttingEdgeAuthorization
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public int Year { get; private set; }
        public AgeRequirement(int year)
        {
            Year = year;
        }
    }
}