using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UNAH_Assistance_Web_API.Models;

namespace UNAH_Assistance_Web_API
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext() : base("MyDbConnectionString")
        {}

        public DbSet<Students> Estudiantes { get; set; }
        public DbSet<Campus> Campus { get; set; }
        public DbSet<UserState> UserStates { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }
        public DbSet<DailyAttendance> DailyAttendances { get; set; }
        public DbSet<PermanentAttendance> PermanentAttendances { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Students>().Property(e => e.IdStudent).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Campus>().Property(e => e.IdCampus).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserState>().Property(e => e.IdUserState).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Teachers>().Property(e => e.IdTeacher).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Classes>().Property(e => e.IdClass).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserTypes>().Property(e => e.IdType).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Students>()
                .HasMany(s => s.Classes)
                .WithMany(c => c.StudentsList)
                .Map(cs =>
                {
                    cs.MapLeftKey("IdStudent");
                    cs.MapRightKey("IdClass");
                    cs.ToTable("StudentClasses");
                });

            modelBuilder.Entity<Students>()
                .HasRequired(s => s.Campus)
                .WithMany()
                .HasForeignKey(s => s.IdCampus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Classes>()
                .HasRequired(c => c.Campus)
                .WithMany()
                .HasForeignKey(c => c.IdCampus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailyAttendance>()
                .HasRequired(d => d.Student)
                .WithMany()
                .HasForeignKey(d => d.IdStudent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailyAttendance>()
                .HasRequired(d => d.Class)
                .WithMany()
                .HasForeignKey(d => d.IdClass)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PermanentAttendance>()
                .HasRequired(p => p.Student)
                .WithMany()
                .HasForeignKey(p => p.IdStudent)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}