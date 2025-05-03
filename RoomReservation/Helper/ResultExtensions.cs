using Microsoft.AspNetCore.Mvc;
using RoomReservation.Shared.Common;

namespace RoomReservation.API.Helper
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new ObjectResult(result.Data) { StatusCode = (int)result.StatusCode };

            var problem = new ProblemDetails
            {
                Title = result.StatusCode.ToString(),
                Detail = result.FailureMessage,
                Status = (int)result.StatusCode
            };

            return new ObjectResult(problem) { StatusCode = (int)result.StatusCode };
        }
        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess)
                return new ObjectResult(true) { StatusCode = (int)result.StatusCode };

            var problem = new ProblemDetails
            {
                Title = result.StatusCode.ToString(),
                Detail = result.FailureMessage,
                Status = (int)result.StatusCode
            };

            return new ObjectResult(problem) { StatusCode = (int)result.StatusCode };
        }
    }
}
