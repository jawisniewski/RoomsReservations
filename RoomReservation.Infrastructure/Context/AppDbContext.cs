using Microsoft.EntityFrameworkCore;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
                r.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                r.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                r.HasMany(r => r.RoomEquipments)
                    .WithOne(re => re.Room)
                    .HasForeignKey(re => re.RoomId);

                r.HasOne(r => r.RoomReservationLimit)
                    .WithOne(r => r.Room)
                    .HasForeignKey<RoomReservationLimit>(r => r.RoomId);

                r.HasMany(r => r.Reservations)
                    .WithOne(r => r.Room)
                    .HasForeignKey(r => r.RoomId);
                r.HasIndex(u => u.Name).IsUnique();
            });

            modelBuilder.Entity<Equipment>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                e.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                e.HasMany(e => e.RoomEquipments)
                    .WithOne(re => re.Equipment)
                    .HasForeignKey(re => re.EquipmentId);
                e.HasData(
                    new Equipment { Id = 1, Name = "Projektor" },
                    new Equipment { Id = 2, Name = "Tablica" },
                    new Equipment { Id = 3, Name = "Zestaw do wideokonferencji" },
                    new Equipment { Id = 4, Name = "Ekran" });
            });

            modelBuilder.Entity<Reservation>(r =>
            {
                r.HasKey(r => r.Id);
                r.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                r.Property(r => r.StartDate)
                    .IsRequired();

                r.Property(r => r.EndDate)
                    .IsRequired();

                r.HasOne(r => r.Room)
                    .WithMany(r => r.Reservations)
                    .HasForeignKey(r => r.RoomId);

                r.HasOne(r => r.User)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserId);
            });

            modelBuilder.Entity<User>(u =>
                u.HasData(
                    new User { Id = 1, Username = "jnowak", Password = "IdioP+/nL+5jf8yWL3tVYWMa5g6sgNIs5w4JFQNXuEs=" },
                    new User { Id = 2, Username = "kkowalski", Password = "Ix7MfReNpfIpg7xXlZk5bWwTmkV5h64e4AJtiEMtanI=" },
                    new User { Id = 3, Username = "unowakowski", Password = "VOoS58R1AHocCK0wA764P3UC5yn2xjOL5ZfEggL9MAU=" })
            );
            modelBuilder.Entity<Room>(u =>
                u.HasData(
                    new Room() { Id = 1, Name = "Klasa", Capacity = 30, RoomLayout = RoomLayoutEnum.Classroom, TableCount = 20 },
                    new Room() { Id = 2, Name = "Sala konferencyjna 5", Capacity = 10, RoomLayout = RoomLayoutEnum.Boardroom, TableCount = 4 },
                    new Room() { Id = 3, Name = "Scena", Capacity = 100, RoomLayout = RoomLayoutEnum.Theater, TableCount = 0 }
            ));
            modelBuilder.Entity<RoomEquipment>(u =>
                u.HasData(
                    new RoomEquipment() { Id = 1, EquipmentId = 1, RoomId = 1, Quantity = 2},
                    new RoomEquipment() { Id = 2, EquipmentId = 2, RoomId = 1, Quantity = 0 },
                    new RoomEquipment() { Id = 3, EquipmentId = 3, RoomId = 1, Quantity = 1 },
                    new RoomEquipment() { Id = 4, EquipmentId = 4, RoomId = 1, Quantity = 1 },
                    new RoomEquipment() { Id = 5, RoomId = 2, EquipmentId = 1, Quantity = 3 },
                    new RoomEquipment() { Id = 6, RoomId = 2, EquipmentId = 3, Quantity = 4 },
                    new RoomEquipment() { Id = 7, RoomId = 3, EquipmentId = 2, Quantity = 1 }
            ));
            modelBuilder.Entity<RoomReservationLimit>(u =>
                u.HasData(
                    new RoomReservationLimit() { Id = 1, RoomId = 1, MaxTime = 90, MinTime= 30 },
                    new RoomReservationLimit() { Id = 2, RoomId = 2, MaxTime = 0, MinTime = 0 },
                    new RoomReservationLimit() { Id = 3, RoomId = 3, MaxTime = 60, MinTime = 0 }
            ));
            modelBuilder.Entity<Reservation>(u =>
                u.HasData(
                    new Reservation() { Id = 1, RoomId = 1, UserId = 1, StartDate = new DateTime(2025, 6, 6, 11, 00, 00), EndDate = new DateTime(2025, 6, 6, 12, 00, 00) },
                    new Reservation() { Id = 2, RoomId = 1, UserId = 1, StartDate = new DateTime(2025, 6, 7, 11, 00, 00), EndDate = new DateTime(2025, 6, 7, 12, 00, 00) },
                    new Reservation() { Id = 3, RoomId = 2, UserId = 1, StartDate = new DateTime(2025, 6, 7, 12, 00, 00), EndDate = new DateTime(2025, 6, 7, 13, 00, 00) },
                    new Reservation() { Id = 4, RoomId = 3, UserId = 2, StartDate = new DateTime(2025, 6, 7, 12, 00, 00), EndDate = new DateTime(2025, 6, 7, 12, 50, 00) },
                    new Reservation() { Id = 5, RoomId = 1, UserId = 3, StartDate = new DateTime(2025, 6, 7, 12, 00, 00), EndDate = new DateTime(2025, 6, 7, 13, 00, 00) }
            ));
        }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomReservationLimit> RoomsReservationLimits { get; set; }
        public DbSet<RoomEquipment> RoomsEquipments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
