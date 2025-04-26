using Microsoft.EntityFrameworkCore;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(r =>
            {
                r.HasKey(r => r.Id);

                r.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                r.HasMany(r => r.RoomsEquipments)
                .WithOne(re => re.Room)
                .HasForeignKey(re => re.RoomId);

                r.HasOne(r => r.RoomReservationLimit)
                    .WithOne(r => r.Room)
                    .HasForeignKey<RoomReservationLimit>(r => r.RoomId);

                r.HasMany(r => r.Reservations)
                    .WithOne(r => r.Room)
                    .HasForeignKey(r => r.RoomId);
            });               

            modelBuilder.Entity<Equipment>(e =>
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                e.HasMany(e => e.RoomsEquipments)
                    .WithOne(re => re.Equipment)
                    .HasForeignKey(re => re.EquipmentId);
            });

            modelBuilder.Entity<Reservation>(r =>
            {
                r.HasKey(r => r.Id);
                r.Property(r => r.StartDate)
                    .IsRequired();
                r.Property(r => r.EndDate)
                    .IsRequired();
                r.HasOne(r => r.Room)
                    .WithMany(r => r.Reservations)
                    .HasForeignKey(r => r.RoomId);
            });
        }
        public DbSet<Equipment> Equipments { get; set; } 
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomReservationLimit> RoomsReservationLimits { get; set; }
        public DbSet<RoomsEquipments> RoomsEquipments { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
