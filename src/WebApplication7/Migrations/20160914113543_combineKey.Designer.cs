using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApplication7.Model;

namespace WebApplication7.Migrations
{
    [DbContext(typeof(WebShopRepository))]
    [Migration("20160914113543_combineKey")]
    partial class combineKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.HasIndex("ProductId");

                    b.ToTable("ProductTranslations");
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

            modelBuilder.Entity("ProductTranslation", b =>
                {
                    b.HasOne("WebApplication7.Model.Product")
                        .WithMany("Translations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
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
