using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApplication7.Model;

namespace WebApplication7.Migrations
{
    [DbContext(typeof(WebShopRepository))]
    partial class WebShopRepositoryModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductTranslation", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<string>("Language");

                    b.Property<string>("ProductDescription")
                        .IsRequired();

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.HasKey("ProductId", "Language");

                    b.ToTable("ProductTranslations");
                });

            modelBuilder.Entity("WebApplication7.Model.Cart", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CartId");

                    b.Property<int>("Count");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("ProductID");

                    b.HasKey("RecordId");

                    b.HasIndex("ProductID");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("WebApplication7.Model.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("Phone");

                    b.Property<string>("PostalCode");

                    b.Property<string>("State");

                    b.Property<decimal>("Total");

                    b.Property<string>("Username");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApplication7.Model.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlbumId");

                    b.Property<int>("OrderId");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebApplication7.Model.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductCategoryId");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebApplication7.Model.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ProductCategoryId1");

                    b.Property<string>("ProductCategoryName");

                    b.HasKey("ProductCategoryId");

                    b.HasIndex("ProductCategoryId1");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("WebApplication7.Model.Cart", b =>
                {
                    b.HasOne("WebApplication7.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication7.Model.OrderDetail", b =>
                {
                    b.HasOne("WebApplication7.Model.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication7.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("WebApplication7.Model.Product", b =>
                {
                    b.HasOne("WebApplication7.Model.ProductCategory", "ProductCategory")
                        .WithMany("Product")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication7.Model.ProductCategory", b =>
                {
                    b.HasOne("WebApplication7.Model.ProductCategory")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductCategoryId1");
                });
        }
    }
}
