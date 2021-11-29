﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NftApi.Data;

#nullable disable

namespace NftApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("NftApi.Data.Models.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PunkzNftEdition")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PunkzNftEdition");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("NftApi.Data.Models.PunkzNft", b =>
                {
                    b.Property<int>("Edition")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccessoriesValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("BackgroundValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("EyesValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("HeadValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImplantNodesValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ipfs")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAuction")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ListedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("MarketId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Minted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("MintedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("MouthValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("OnSale")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SalePrice")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Score")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeValue")
                        .HasColumnType("TEXT");

                    b.HasKey("Edition");

                    b.HasIndex("AccessoriesValue");

                    b.HasIndex("BackgroundValue");

                    b.HasIndex("EyesValue");

                    b.HasIndex("HeadValue");

                    b.HasIndex("ImplantNodesValue");

                    b.HasIndex("MouthValue");

                    b.HasIndex("TypeValue");

                    b.ToTable("PunkzNfts");
                });

            modelBuilder.Entity("NftApi.Data.Models.Trait", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Percent")
                        .HasColumnType("TEXT");

                    b.HasKey("Value");

                    b.ToTable("Traits");
                });

            modelBuilder.Entity("NftApi.Data.Models.Offer", b =>
                {
                    b.HasOne("NftApi.Data.Models.PunkzNft", null)
                        .WithMany("Offers")
                        .HasForeignKey("PunkzNftEdition");
                });

            modelBuilder.Entity("NftApi.Data.Models.PunkzNft", b =>
                {
                    b.HasOne("NftApi.Data.Models.Trait", "Accessories")
                        .WithMany()
                        .HasForeignKey("AccessoriesValue");

                    b.HasOne("NftApi.Data.Models.Trait", "Background")
                        .WithMany()
                        .HasForeignKey("BackgroundValue");

                    b.HasOne("NftApi.Data.Models.Trait", "Eyes")
                        .WithMany()
                        .HasForeignKey("EyesValue");

                    b.HasOne("NftApi.Data.Models.Trait", "Head")
                        .WithMany()
                        .HasForeignKey("HeadValue");

                    b.HasOne("NftApi.Data.Models.Trait", "ImplantNodes")
                        .WithMany()
                        .HasForeignKey("ImplantNodesValue");

                    b.HasOne("NftApi.Data.Models.Trait", "Mouth")
                        .WithMany()
                        .HasForeignKey("MouthValue");

                    b.HasOne("NftApi.Data.Models.Trait", "Type")
                        .WithMany()
                        .HasForeignKey("TypeValue");

                    b.Navigation("Accessories");

                    b.Navigation("Background");

                    b.Navigation("Eyes");

                    b.Navigation("Head");

                    b.Navigation("ImplantNodes");

                    b.Navigation("Mouth");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("NftApi.Data.Models.PunkzNft", b =>
                {
                    b.Navigation("Offers");
                });
#pragma warning restore 612, 618
        }
    }
}
