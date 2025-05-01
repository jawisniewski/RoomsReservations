using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Shared.Common;
using RoomReservation.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Shared.DependencyInjection
{
    public static class SharedModule
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddScoped(typeof(IHasher), typeof(Hasher));

            return services;
        }
    }
}
