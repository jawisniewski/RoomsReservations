using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Application.Services;

namespace RoomReservation.Infrastructure.DependencyInjection
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
       
            services.AddScoped(typeof(IRoomService), typeof(RoomService));
            services.AddScoped(typeof(IReservationService), typeof(ReservationService));
            services.AddScoped(typeof(IUserService), typeof(UserService));

            return services;
        }
    }
}
