using System.Security.Claims;

namespace DATN.WebAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Guid.Empty;

            return Guid.Parse(userIdClaim.Value);
        }
    }
} 