using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    public partial class OfferModelRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "Offer",
                table: "Offers",
                newName: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Offers",
                newName: "Offer");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Offers",
                type: "TEXT",
                nullable: true);
        }
    }
}
