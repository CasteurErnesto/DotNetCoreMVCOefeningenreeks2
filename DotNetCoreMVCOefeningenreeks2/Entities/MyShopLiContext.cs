using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DotNetCoreMVCOefeningenreeks2.Entities
{
    public partial class MyShopLiContext : DbContext
    {
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<ShopItem> ShopItem { get; set; }

        public MyShopLiContext(DbContextOptions<MyShopLiContext> options): base(options)
        {

        }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyShopLi;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ShopItem>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.ShopItem)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_ShopItem_Cart");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ShopItem)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ShopItem_Category");
            });
        }
    }
}
