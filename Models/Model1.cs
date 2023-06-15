using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Fin.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model124")
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Business_User> Business_User { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<COMMENT> COMMENTs { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<PACKAGE> PACKAGEs { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public object Booking { get; internal set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.Admin_Password)
                .IsFixedLength();

            modelBuilder.Entity<Business_User>()
                .Property(e => e.Bus_Password)
                .IsFixedLength();

            modelBuilder.Entity<Business_User>()
                .HasMany(e => e.PACKAGEs)
                .WithRequired(e => e.Business_User)
                .HasForeignKey(e => e.FID_Business_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.PACKAGEs)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.Category_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Destination>()
                .HasMany(e => e.PACKAGEs)
                .WithRequired(e => e.Destination)
                .HasForeignKey(e => e.Destination_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.PACKAGE)
                .HasForeignKey(e => e.Package_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.PACKAGE)
                .HasForeignKey(e => e.Package_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.COMMENTs)
                .WithRequired(e => e.PACKAGE)
                .HasForeignKey(e => e.Package_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.Places)
                .WithRequired(e => e.PACKAGE)
                .HasForeignKey(e => e.Package_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.WishLists)
                .WithRequired(e => e.PACKAGE)
                .HasForeignKey(e => e.Package_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.User_Gender)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.COMMENTs)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.WishLists)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_FID)
                .WillCascadeOnDelete(false);
        }
    }
}
