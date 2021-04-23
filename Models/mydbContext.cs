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
                optionsBuilder.UseMySql("server=localhost;port=3306;database=mydb;user=root;password=Matkokat123!", x => x.ServerVersion("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branches>(entity =>
            {
                entity.ToTable("branches");

                entity.HasIndex(e => e.BranchesId)
                    .HasName("branches_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.BranchesId).HasColumnName("branches_id");

                entity.Property(e => e.BranchesAddress)
                    .HasColumnName("branches_address")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BranchesPhone)
                    .IsRequired()
                    .HasColumnName("branches_phone")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<CreditCarts>(entity =>
            {
                entity.HasKey(e => e.CreditCartId)
                    .HasName("PRIMARY");

                entity.ToTable("credit_carts");

                entity.HasIndex(e => e.CreditCartId)
                    .HasName("credit_cart_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CustomerCustomerId)
                    .HasName("fk_Credit_cart_Customer1_idx");

                entity.Property(e => e.CreditCartId).HasColumnName("credit_cart_id");

                entity.Property(e => e.CreditcartExpiration)
                    .HasColumnName("creditcart_expiration")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreditcartExtradetails)
                    .HasColumnName("creditcart_extradetails")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreditcartValidation)
                    .IsRequired()
                    .HasColumnName("creditcart_validation")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustomerCustomerId).HasColumnName("Customer_customer_id");

                entity.HasOne(d => d.CustomerCustomer)
                    .WithMany(p => p.CreditCarts)
                    .HasForeignKey(d => d.CustomerCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Credit_cart_Customer1");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PRIMARY");

                entity.ToTable("customers");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("customer_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerAddress)
                    .IsRequired()
                    .HasColumnName("customer_address")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasColumnName("customer_email")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustomerFirstname)
                    .IsRequired()
                    .HasColumnName("customer_firstname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustomerPassword)
                    .IsRequired()
                    .HasColumnName("customer_password")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustomerPhone)
                    .IsRequired()
                    .HasColumnName("customer_phone")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustomerSurname)
                    .IsRequired()
                    .HasColumnName("customer_surname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.BranchesBranchesId)
                    .HasName("fk_Orders_Branches1_idx");

                entity.HasIndex(e => e.CustomersCustomerId)
                    .HasName("fk_orders_customers1_idx");

                entity.HasIndex(e => e.OrdersId)
                    .HasName("orders_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PaymentId)
                    .HasName("fk_Orders_Payment1_idx");

                entity.Property(e => e.OrdersId).HasColumnName("orders_id");

                entity.Property(e => e.BranchesBranchesId).HasColumnName("Branches_branches_id");

                entity.Property(e => e.CartCartId).HasColumnName("Cart_cart_id");

                entity.Property(e => e.CustomersCustomerId).HasColumnName("customers_customer_id");

                entity.Property(e => e.OrdersFullAmount)
                    .HasColumnName("orders_full_amount")
                    .HasColumnType("decimal(14,0)");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.HasOne(d => d.BranchesBranches)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BranchesBranchesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_Branches1");

                entity.HasOne(d => d.CustomersCustomer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomersCustomerId)
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
                entity.HasKey(e => e.PaymentId)
                    .HasName("PRIMARY");

                entity.ToTable("payments");

                entity.HasIndex(e => e.CreditCartId)
                    .HasName("fk_Payment_Credit_cart1_idx");

                entity.HasIndex(e => e.PaymentId)
                    .HasName("payment_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.CreditCartId).HasColumnName("credit_cart_id");

                entity.Property(e => e.PaymentInvoice)
                    .HasColumnName("payment_invoice")
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.CreditCart)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CreditCartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Payment_Credit_cart1");
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.ToTable("product_categories");

                entity.HasIndex(e => e.ProductCategoriesId)
                    .HasName("product_categories_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ProductCategoriesId).HasColumnName("product_categories_id");

                entity.Property(e => e.ProductCategoriesDescription)
                    .HasColumnName("product_categories_description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductCategoriesType)
                    .HasColumnName("product_categories_type")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.ProductsId)
                    .HasName("products_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SuppliersId)
                    .HasName("fk_Products_Suppliers_idx");

                entity.Property(e => e.ProductsId).HasColumnName("products_id");

                entity.Property(e => e.ProductDescription)
                    .HasColumnName("product_description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductImageData)
                    .HasColumnName("product_image_data")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductImageName)
                    .HasColumnName("product_image_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductPrice)
                    .HasColumnName("product_price")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.ProductWeight)
                    .HasColumnName("product_weight")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.SuppliersId).HasColumnName("suppliers_id");

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

                entity.Property(e => e.ProductsId).HasColumnName("products_id");

                entity.Property(e => e.ProductCategoriesId).HasColumnName("product_categories_id");

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
                entity.HasKey(e => new { e.ProductsProductsId, e.OrdersOrdersId })
                    .HasName("PRIMARY");

                entity.ToTable("products_orders");

                entity.HasIndex(e => e.OrdersOrdersId)
                    .HasName("fk_products_has_orders_orders1_idx");

                entity.HasIndex(e => e.ProductsProductsId)
                    .HasName("fk_products_has_orders_products1_idx");

                entity.Property(e => e.ProductsProductsId).HasColumnName("products_products_id");

                entity.Property(e => e.OrdersOrdersId).HasColumnName("orders_orders_id");

                entity.HasOne(d => d.OrdersOrders)
                    .WithMany(p => p.ProductsOrders)
                    .HasForeignKey(d => d.OrdersOrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_has_orders_orders1");

                entity.HasOne(d => d.ProductsProducts)
                    .WithMany(p => p.ProductsOrders)
                    .HasForeignKey(d => d.ProductsProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_has_orders_products1");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.HasIndex(e => e.BranchesBranchesId)
                    .HasName("fk_Staff_Branches1_idx");

                entity.HasIndex(e => e.StaffId)
                    .HasName("staff_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.BranchesBranchesId).HasColumnName("Branches_branches_id");

                entity.Property(e => e.StaffAddress)
                    .HasColumnName("staff_address")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffFullname)
                    .IsRequired()
                    .HasColumnName("staff_fullname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffPhone)
                    .HasColumnName("staff_phone")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffPosition)
                    .IsRequired()
                    .HasColumnName("staff_position")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.BranchesBranches)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.BranchesBranchesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Staff_Branches1");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.ToTable("suppliers");

                entity.HasIndex(e => e.SuppliersId)
                    .HasName("suppliers_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SuppliersId).HasColumnName("suppliers_id");

                entity.Property(e => e.SuppliersAddress)
                    .IsRequired()
                    .HasColumnName("suppliers_address")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SuppliersName)
                    .IsRequired()
                    .HasColumnName("suppliers_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
