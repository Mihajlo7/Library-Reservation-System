using LibrarySystem.Entities;
using LibrarySystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.DataAccessLayer.Persistence
{
    public sealed class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Book Model
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b=>b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b=>b.Author).IsRequired().HasMaxLength(100);
                entity.Property(b=>b.Isbn).IsRequired();
                entity.HasIndex(b=>b.Isbn).IsUnique();
                entity.Property(b => b.Genre).HasConversion<string>();
            });

        }

        public override int SaveChanges()
        {
            var entites = ChangeTracker.Entries<IEntity>();

            foreach (var entity in entites)
            {

                // If operation is insert
                if(entity.State == EntityState.Added)
                {
                    entity.Entity.Id = Guid.NewGuid();
                    entity.Entity.CreatedAt = DateTime.UtcNow;
                    entity.Entity.UpdatedAt = DateTime.UtcNow;
                }

                // If operation is update
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}
