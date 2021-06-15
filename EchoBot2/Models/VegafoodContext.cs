using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EchoBot2.Models
{
    public partial class VegafoodContext : DbContext
    {
        public VegafoodContext()
        {
        }

        public VegafoodContext(DbContextOptions<VegafoodContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerSentiment> CustomerSentiments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuDetail> MenuDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<RefreshTokenEmployee> RefreshTokenEmployees { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-N30SPMS;Initial Catalog=Vegafood;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(15);

                entity.Property(e => e.Description).HasColumnType("ntext");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CustomerName).HasMaxLength(30);

                entity.Property(e => e.District).HasMaxLength(15);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(15);

                entity.Property(e => e.Phone).HasMaxLength(10);
            });

            modelBuilder.Entity<CustomerSentiment>(entity =>
            {
                entity.ToTable("CustomerSentiment");

                entity.Property(e => e.FoodComment).HasMaxLength(30);

                entity.Property(e => e.FoodPredict)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceComment).HasMaxLength(30);

                entity.Property(e => e.ServicePredict)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(30);

                entity.Property(e => e.VegaComment).HasMaxLength(30);

                entity.Property(e => e.VegaPredict)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.District).HasMaxLength(15);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.Notes).HasColumnType("ntext");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.Property(e => e.Photo).HasColumnType("ntext");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Images).HasColumnType("ntext");

                entity.Property(e => e.MenuDescription).HasColumnType("ntext");

                entity.Property(e => e.MenuName).HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<MenuDetail>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.MenuId })
                    .HasName("PK__Menu Det__A8952BC885DC7C0A");

                entity.ToTable("Menu Details");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuDetails)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu Details_Menus");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MenuDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu Details_Products");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipDistrict).HasMaxLength(15);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.PaymentViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentVia)
                    .HasConstraintName("FK_Orders_Payments");

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_Orders_Shippers");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.MenuId })
                    .HasName("PK__Order De__DF09B68AE59083DE");

                entity.ToTable("Order Details");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order Details_Menus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order Details_Orders");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.PayCompany).HasMaxLength(50);

                entity.Property(e => e.PaymentSolution).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Images).HasColumnType("ntext");

                entity.Property(e => e.ProductDescription).HasColumnType("ntext");

                entity.Property(e => e.ProductName).HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("RefreshToken");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry_date");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__RefreshTo__custo__5535A963");
            });

            modelBuilder.Entity<RefreshTokenEmployee>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("RefreshTokenEmployee");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry_date");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RefreshTokenEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__RefreshTo__emplo__5629CD9C");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

                entity.Property(e => e.CompanyName).HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
