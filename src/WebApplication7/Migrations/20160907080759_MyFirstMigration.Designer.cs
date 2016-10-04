using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApplication7.Model;

namespace WebApplication7.Migrations
{
    [DbContext(typeof(WebShopRepository))]
    [Migration("20160907080759_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication7.Model.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ProductCategoryId1");

                    b.Property<int>("ProductCategoryName");

                    b.HasKey("ProductCategoryId");

                    b.HasIndex("ProductCategoryId1");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("WebApplication7.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductCategoryId");

                    b.Property<string>("ProductName");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebApplication7.Model.ProductCategory", b =>
                {
                    b.HasOne("WebApplication7.Model.ProductCategory")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductCategoryId1");
                });

            modelBuilder.Entity("WebApplication7.Product", b =>
                {
                    b.HasOne("WebApplication7.Model.ProductCategory", "ProductCategory")
                        .WithMany("Product")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
