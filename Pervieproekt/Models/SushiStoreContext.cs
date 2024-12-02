using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pervieproekt.Models;

public partial class SushiStoreContext : DbContext
{
    public SushiStoreContext()
    {
    }

    public SushiStoreContext(DbContextOptions<SushiStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalogg> Cataloggs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<PosOrder> PosOrders { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-P4T6AUH\\SQLEXPRESS;Initial Catalog=SushiStore;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalogg>(entity =>
        {
            entity.HasKey(e => e.IdCatalog).HasName("PK__Catalogg__38D620C56A42134E");

            entity.ToTable("Catalogg");

            entity.Property(e => e.IdCatalog).HasColumnName("ID_Catalog");
            entity.Property(e => e.Img)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Information)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Namee)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductTypeId).HasColumnName("ProductType_ID");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Cataloggs)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Catalogg__Produc__403A8C7D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__EC9FA955E653AEB8");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentType_ID");
            entity.Property(e => e.PosOrdersId).HasColumnName("PosOrders_ID");
            entity.Property(e => e.Summ).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__PaymentT__4BAC3F29");

            entity.HasOne(d => d.PosOrders).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PosOrdersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__PosOrder__48CFD27E");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.IdPaymentType).HasName("PK__PaymentT__874EC9E7360A7BEB");

            entity.HasIndex(e => e.PaymentType1, "UQ__PaymentT__262372780DA1C087").IsUnique();

            entity.Property(e => e.IdPaymentType).HasColumnName("ID_PaymentType");
            entity.Property(e => e.PaymentType1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PaymentType");
        });

        modelBuilder.Entity<PosOrder>(entity =>
        {
            entity.HasKey(e => e.IdPosOrders).HasName("PK__PosOrder__D41EBBEEB48E0350");

            entity.Property(e => e.IdPosOrders).HasColumnName("ID_PosOrders");
            entity.Property(e => e.CatalogId).HasColumnName("Catalog_ID");
            entity.Property(e => e.Price).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.UserrId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Userr_ID");

            entity.HasOne(d => d.Catalog).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PosOrders__Catal__45F365D3");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.IdProductType).HasName("PK__ProductT__BF69275896A2D2B1");

            entity.HasIndex(e => e.TypeName, "UQ__ProductT__D4E7DFA8922F8248").IsUnique();

            entity.Property(e => e.IdProductType).HasColumnName("ID_ProductType");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReviews).HasName("PK__Reviews__42F208838B51EABC");

            entity.Property(e => e.IdReviews).HasColumnName("ID_Reviews");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.Review1)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("Review");
            entity.Property(e => e.UserrId).HasColumnName("Userr_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Order_I__4F7CD00D");

            entity.HasOne(d => d.Userr).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Userr_I__4E88ABD4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__ED4DE4423930B881");

            entity.HasIndex(e => new { e.Loginn, e.Passwordd }, "UQ__Users__11F90C9272EFB0F4").IsUnique();

            entity.HasIndex(e => e.Mail, "UQ__Users__2724B2D19F16DD8F").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Loginn)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Namee)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Passwordd)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
