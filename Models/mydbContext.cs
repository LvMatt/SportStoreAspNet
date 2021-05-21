using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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
        public virtual DbSet<Creditcards> Creditcards { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Orderdetails> Orderdetails { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Productcategories> Productcategories { get; set; }
        public virtual DbSet<Productratings> Productratings { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=mydb;uid=root;pwd=Matkokat123!", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mydbContext>().HasNoKey();

            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Branches>(entity =>
            {
                entity.ToTable("branches");

                entity.HasIndex(e => e.Id, "branches_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(45);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Creditcards>(entity =>
            {
                entity.ToTable("creditcards");

                entity.HasIndex(e => e.Id, "credit_cart_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CustomersId, "fk_creditCards_customers1_idx");

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.Extradetails).HasMaxLength(45);

                entity.Property(e => e.Validation)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Creditcards)
                    .HasForeignKey(d => d.CustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_creditCards_customers1");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.Id, "customer_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.BranchesId, "fk_Orders_Branches1_idx");

                entity.HasIndex(e => e.PaymentId, "fk_Orders_Payment1_idx");

                entity.HasIndex(e => e.CustomersId, "fk_orders_customers1_idx");

                entity.HasIndex(e => e.Id, "orders_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Amount).HasPrecision(14);

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

            modelBuilder.Entity<Orderdetails>(entity =>
            {
                entity.ToTable("orderdetails");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.OrdersId, "fk_orderDetails_orders1_idx");

                entity.HasIndex(e => e.ProductsId, "fk_orderDetails_products1_idx");

                entity.HasOne(d => d.Orders)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.OrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderDetails_orders1");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderDetails_products1");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.ToTable("payments");

                entity.HasIndex(e => e.CreditcartId, "fk_payments_creditcards1_idx");

                entity.HasIndex(e => e.Id, "payment_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PaymentInvoice).HasMaxLength(1000);

                //entity.HasOne(d => d.CreditcartId)
                //    .WithMany(p => p.Payments)
                //    .HasForeignKey(d => d.CreditCardId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_payments_creditcards1");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.SuppliersId, "fk_Products_Suppliers_idx");

                entity.HasIndex(e => e.ProductCategoriesId, "fk_products_product_categories1_idx");

                entity.HasIndex(e => e.Id, "products_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageData).HasMaxLength(50);

                entity.Property(e => e.ImageName).HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Price).HasPrecision(10);

                entity.Property(e => e.Weight).HasPrecision(10);

                entity.HasOne(d => d.ProductCategories)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_product_categories1");

                entity.HasOne(d => d.Suppliers)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SuppliersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Products_Suppliers");
            });

            modelBuilder.Entity<Productcategories>(entity =>
            {
                entity.ToTable("productcategories");

                entity.HasIndex(e => e.Id, "product_categories_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(45);
            });

            modelBuilder.Entity<Productratings>(entity =>
            {
                entity.ToTable("productratings");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProductsId, "fk_productRatings_products1_idx");

                entity.HasIndex(e => e.CustomersId, "fk_productratings_customers1_idx");

                entity.Property(e => e.Comment).HasColumnType("text");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Productratings)
                    .HasForeignKey(d => d.CustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_productratings_customers1");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.Productratings)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_productRatings_products1");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.ToTable("suppliers");

                entity.HasIndex(e => e.Id, "suppliers_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasIndex(e => e.BranchesId, "fk_Staff_Branches1_idx");

                entity.HasIndex(e => e.Id, "staff_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(45);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Phone).HasMaxLength(45);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.Branches)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.BranchesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Staff_Branches1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
