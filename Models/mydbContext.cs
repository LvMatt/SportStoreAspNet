using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SportStore.Models
{
    public partial class mydbContext : DbContext
    {
        public mydbContext()
        {
        }

        public mydbContext(DbContextOptions<mydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<CreditCarts> CreditCarts { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsCategories> ProductsCategories { get; set; }
        public virtual DbSet<ProductsOrders> ProductsOrders { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=mydb;uid=root;pwd=Matkokat123!", x => x.ServerVersion("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branches>(entity =>
            {
                entity.ToTable("branches");

                entity.HasIndex(e => e.Id)
                    .HasName("branches_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<CreditCarts>(entity =>
            {
                entity.ToTable("credit_carts");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("fk_Credit_cart_Customer1_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("credit_cart_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.Extradetails)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Validation)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CreditCarts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Credit_cart_Customer1");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.Id)
                    .HasName("customer_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.BranchesId)
                    .HasName("fk_Orders_Branches1_idx");

                entity.HasIndex(e => e.CustomersId)
                    .HasName("fk_orders_customers1_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("orders_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PaymentId)
                    .HasName("fk_Orders_Payment1_idx");

                entity.Property(e => e.Amount).HasColumnType("decimal(14,0)");

                entity.HasOne(d => d.Branches)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BranchesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_Branches1");

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orders_customers1");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_Payment1");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.ToTable("payments");

                entity.HasIndex(e => e.CreditcartId)
                    .HasName("fk_Payment_Credit_cart1_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("payment_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PaymentInvoice)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Creditcart)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CreditcartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Payment_Credit_cart1");
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.ToTable("product_categories");

                entity.HasIndex(e => e.Id)
                    .HasName("product_categories_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.Id)
                    .HasName("products_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SuppliersId)
                    .HasName("fk_Products_Suppliers_idx");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ImageData).HasMaxLength(50);

                entity.Property(e => e.ImageName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Price).HasColumnType("decimal(10,0)");

                entity.Property(e => e.Weight).HasColumnType("decimal(10,0)");

                entity.HasOne(d => d.Suppliers)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SuppliersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Products_Suppliers");
            });

            modelBuilder.Entity<ProductsCategories>(entity =>
            {
                entity.HasKey(e => new { e.ProductsId, e.ProductCategoriesId })
                    .HasName("PRIMARY");

                entity.ToTable("products_categories");

                entity.HasIndex(e => e.ProductCategoriesId)
                    .HasName("fk_products_has_product_categories_product_categories1_idx");

                entity.HasIndex(e => e.ProductsId)
                    .HasName("fk_products_has_product_categories_products1_idx");

                entity.HasOne(d => d.ProductCategories)
                    .WithMany(p => p.ProductsCategories)
                    .HasForeignKey(d => d.ProductCategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_has_product_categories_product_categories1");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.ProductsCategories)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_has_product_categories_products1");
            });

            modelBuilder.Entity<ProductsOrders>(entity =>
            {
                entity.HasKey(e => new { e.ProductsId, e.OrdersId })
                    .HasName("PRIMARY");

                entity.ToTable("products_orders");

                entity.HasIndex(e => e.OrdersId)
                    .HasName("fk_products_has_orders_orders1_idx");

                entity.HasIndex(e => e.ProductsId)
                    .HasName("fk_products_has_orders_products1_idx");

                entity.HasOne(d => d.Orders)
                    .WithMany(p => p.ProductsOrders)
                    .HasForeignKey(d => d.OrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_has_orders_orders1");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.ProductsOrders)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_has_orders_products1");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.HasIndex(e => e.BranchesId)
                    .HasName("fk_Staff_Branches1_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("staff_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Branches)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.BranchesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Staff_Branches1");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.ToTable("suppliers");

                entity.HasIndex(e => e.Id)
                    .HasName("suppliers_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
