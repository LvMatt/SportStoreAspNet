using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SportStore.Model
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
        public virtual DbSet<Orderdetails> Orderdetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<Productratings> Productratings { get; set; }
        public virtual DbSet<Products> Products { get; set; }
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

            modelBuilder.Entity<Creditcards>(entity =>
            {
                entity.ToTable("creditcards");

                entity.HasIndex(e => e.CustomersId)
                    .HasName("fk_creditCards_customers1_idx");

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

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Creditcards)
                    .HasForeignKey(d => d.CustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_creditCards_customers1");
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

            modelBuilder.Entity<Orderdetails>(entity =>
            {
                entity.ToTable("orderdetails");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.OrdersId)
                    .HasName("fk_orderDetails_orders1_idx");

                entity.HasIndex(e => e.ProductsId)
                    .HasName("fk_orderDetails_products1_idx");

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

            modelBuilder.Entity<Productratings>(entity =>
            {
                entity.ToTable("productratings");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProductsId)
                    .HasName("fk_productRatings_products1_idx");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.Productratings)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_productRatings_products1");
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
