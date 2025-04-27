using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Infrastructure.Context;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Infrastructure.Repositories;

namespace RoomReservation.Infrastructure.DependencyInjection
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRoomRepository), typeof(RoomRepository));
            return services;
        }
    }
}
