using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NftApi.Migrations
{
    public partial class AddCnftIoOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAuction",
                table: "PunkzNfts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    User = table.Column<string>(type: "TEXT", nullable: true),
                    Offer = table.Column<long>(type: "INTEGER", nullable: false),
                    Expires = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PunkzNftEdition = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_PunkzNfts_PunkzNftEdition",
                        column: x => x.PunkzNftEdition,
                        principalTable: "PunkzNfts",
                        principalColumn: "Edition",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PunkzNftEdition",
                table: "Offers",
                column: "PunkzNftEdition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropColumn(
                name: "IsAuction",
                table: "PunkzNfts");
        }
    }
}
