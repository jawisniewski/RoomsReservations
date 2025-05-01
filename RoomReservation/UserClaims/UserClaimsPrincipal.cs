using System.Security.Claims;

namespace RoomReservation.API.UserClaims
{
    public static class UserClaimsPrincipal
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }

            return 0;
        }
    }
}
