using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Shared.Common
{
    public static class MapToActionResult
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new ObjectResult(result.Data) { StatusCode = (int)result.StatusCode };

            return new ObjectResult(result.Exception) { StatusCode = (int)result.StatusCode };
        }
    }
}
