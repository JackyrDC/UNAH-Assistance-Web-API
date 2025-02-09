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
        public DbSet<Rolls> Rolls { get; set; }
        public DbSet<DailyRoll> DailyRolls { get; set; }
        public DbSet<PermanentRolls> PermanentRolls { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar el tamaño del lote de operaciones
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
                .WillCascadeOnDelete(false)
            modelBuilder.Entity<Rolls>()
                .HasRequired(r => r.Teacher)
                .WithMany()
                .HasForeignKey(r => r.IdTeacher)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rolls>()
                .HasRequired(r => r.Class)
                .WithMany()
                .HasForeignKey(r => r.IdClass)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rolls>()
                .HasMany(r => r.DailyRolls)
                .WithRequired(dr => dr.Roll)
                .HasForeignKey(dr => dr.IdRoll)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailyRoll>()
                .HasMany(dr => dr.StudentsList)
                .WithRequired(pr => pr.DailyRoll)
                .HasForeignKey(pr => pr.IdDailyRoll)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PermanentRolls>()
                .HasKey(pr => new { pr.IdDailyRoll, pr.IdStudent });

            modelBuilder.Entity<PermanentRolls>()
                .HasRequired(pr => pr.Student)
                .WithMany()
                .HasForeignKey(pr => pr.IdStudent)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}