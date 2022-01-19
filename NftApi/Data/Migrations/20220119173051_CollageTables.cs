using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    public partial class CollageTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Traits_AccessoriesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Traits_BackgroundValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Traits_EyesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Traits_HeadValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Traits_ImplantNodesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Traits_MouthValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Traits_TypeValue",
                table: "PunkzNfts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Traits",
                table: "Traits");

            migrationBuilder.RenameTable(
                name: "Traits",
                newName: "Trait");

            migrationBuilder.AddColumn<int>(
                name: "CollageNftEdition",
                table: "Offers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trait",
                table: "Trait",
                column: "Value");

            migrationBuilder.CreateTable(
                name: "CollageNfts",
                columns: table => new
                {
                    Edition = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FrameValue = table.Column<string>(type: "TEXT", nullable: true),
                    TypeValue = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Score = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Ipfs = table.Column<string>(type: "TEXT", nullable: true),
                    Minted = table.Column<bool>(type: "INTEGER", nullable: false),
                    MintedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OnSale = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAuction = table.Column<bool>(type: "INTEGER", nullable: false),
                    SalePrice = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ListedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MarketName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollageNfts", x => x.Edition);
                    table.ForeignKey(
                        name: "FK_CollageNfts_Trait_FrameValue",
                        column: x => x.FrameValue,
                        principalTable: "Trait",
                        principalColumn: "Value");
                    table.ForeignKey(
                        name: "FK_CollageNfts_Trait_TypeValue",
                        column: x => x.TypeValue,
                        principalTable: "Trait",
                        principalColumn: "Value");
                });

            migrationBuilder.CreateTable(
                name: "CollageWhitelist",
                columns: table => new
                {
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollageWhitelist", x => x.Value);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CollageNftEdition",
                table: "Offers",
                column: "CollageNftEdition");

            migrationBuilder.CreateIndex(
                name: "IX_CollageNfts_FrameValue",
                table: "CollageNfts",
                column: "FrameValue");

            migrationBuilder.CreateIndex(
                name: "IX_CollageNfts_TypeValue",
                table: "CollageNfts",
                column: "TypeValue");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_CollageNfts_CollageNftEdition",
                table: "Offers",
                column: "CollageNftEdition",
                principalTable: "CollageNfts",
                principalColumn: "Edition");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Trait_AccessoriesValue",
                table: "PunkzNfts",
                column: "AccessoriesValue",
                principalTable: "Trait",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Trait_BackgroundValue",
                table: "PunkzNfts",
                column: "BackgroundValue",
                principalTable: "Trait",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Trait_EyesValue",
                table: "PunkzNfts",
                column: "EyesValue",
                principalTable: "Trait",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Trait_HeadValue",
                table: "PunkzNfts",
                column: "HeadValue",
                principalTable: "Trait",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Trait_ImplantNodesValue",
                table: "PunkzNfts",
                column: "ImplantNodesValue",
                principalTable: "Trait",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Trait_MouthValue",
                table: "PunkzNfts",
                column: "MouthValue",
                principalTable: "Trait",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Trait_TypeValue",
                table: "PunkzNfts",
                column: "TypeValue",
                principalTable: "Trait",
                principalColumn: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_CollageNfts_CollageNftEdition",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Trait_AccessoriesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Trait_BackgroundValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Trait_EyesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Trait_HeadValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Trait_ImplantNodesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Trait_MouthValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_Trait_TypeValue",
                table: "PunkzNfts");

            migrationBuilder.DropTable(
                name: "CollageNfts");

            migrationBuilder.DropTable(
                name: "CollageWhitelist");

            migrationBuilder.DropIndex(
                name: "IX_Offers_CollageNftEdition",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trait",
                table: "Trait");

            migrationBuilder.DropColumn(
                name: "CollageNftEdition",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Trait",
                newName: "Traits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traits",
                table: "Traits",
                column: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Traits_AccessoriesValue",
                table: "PunkzNfts",
                column: "AccessoriesValue",
                principalTable: "Traits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Traits_BackgroundValue",
                table: "PunkzNfts",
                column: "BackgroundValue",
                principalTable: "Traits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Traits_EyesValue",
                table: "PunkzNfts",
                column: "EyesValue",
                principalTable: "Traits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Traits_HeadValue",
                table: "PunkzNfts",
                column: "HeadValue",
                principalTable: "Traits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Traits_ImplantNodesValue",
                table: "PunkzNfts",
                column: "ImplantNodesValue",
                principalTable: "Traits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Traits_MouthValue",
                table: "PunkzNfts",
                column: "MouthValue",
                principalTable: "Traits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_Traits_TypeValue",
                table: "PunkzNfts",
                column: "TypeValue",
                principalTable: "Traits",
                principalColumn: "Value");
        }
    }
}
