﻿// <auto-generated />
using System;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CocktailAppBackend.Migrations
{
    [DbContext(typeof(CocktailAppDBContext))]
    [Migration("20231021211912_FinalCreate")]
    partial class FinalCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AuthRecipe", b =>
                {
                    b.Property<int>("FavouritedId")
                        .HasColumnType("int");

                    b.Property<int>("FavouritesId")
                        .HasColumnType("int");

                    b.HasKey("FavouritedId", "FavouritesId");

                    b.HasIndex("FavouritesId");

                    b.ToTable("AuthRecipe");
                });

            modelBuilder.Entity("CocktailApp.Models.Auth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Auths");
                });

            modelBuilder.Entity("CocktailApp.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Kcal")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("CocktailApp.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("AuthId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Note")
                        .HasColumnType("longtext");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AuthId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CocktailApp.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("RatedById")
                        .HasColumnType("int");

                    b.Property<int>("RatedRecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RatedById");

                    b.HasIndex("RatedRecipeId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("CocktailApp.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CocktailApp.Models.RecipeDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AmountInOz")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeDetails");
                });

            modelBuilder.Entity("CocktailApp.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.Property<int>("RecipesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("RecipesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("RecipeTag");
                });

            modelBuilder.Entity("AuthRecipe", b =>
                {
                    b.HasOne("CocktailApp.Models.Auth", null)
                        .WithMany()
                        .HasForeignKey("FavouritedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("FavouritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CocktailApp.Models.Order", b =>
                {
                    b.HasOne("CocktailApp.Models.Auth", "CreatedByUser")
                        .WithMany("OrderList")
                        .HasForeignKey("AuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.Recipe", "Recipe")
                        .WithMany("Orders")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("CocktailApp.Models.Rating", b =>
                {
                    b.HasOne("CocktailApp.Models.Auth", "RatedBy")
                        .WithMany()
                        .HasForeignKey("RatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.Recipe", "RatedRecipe")
                        .WithMany("Ratings")
                        .HasForeignKey("RatedRecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RatedBy");

                    b.Navigation("RatedRecipe");
                });

            modelBuilder.Entity("CocktailApp.Models.RecipeDetail", b =>
                {
                    b.HasOne("CocktailApp.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeDetails")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.Recipe", "Recipe")
                        .WithMany("RecipeDetails")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.HasOne("CocktailApp.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailApp.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CocktailApp.Models.Auth", b =>
                {
                    b.Navigation("OrderList");
                });

            modelBuilder.Entity("CocktailApp.Models.Ingredient", b =>
                {
                    b.Navigation("RecipeDetails");
                });

            modelBuilder.Entity("CocktailApp.Models.Recipe", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Ratings");

                    b.Navigation("RecipeDetails");
                });
#pragma warning restore 612, 618
        }
    }
}