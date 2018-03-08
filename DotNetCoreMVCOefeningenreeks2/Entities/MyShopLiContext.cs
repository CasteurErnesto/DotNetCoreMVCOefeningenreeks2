using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{
    public partial class MyShopLiContext : DbContext
    {
        public virtual DbSet<ShopItem> ShopItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyShopLi;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopItem>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
