using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    public partial class MultipleMarkets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MarketId",
                table: "PunkzNfts",
                newName: "MarketUrl");

            migrationBuilder.AddColumn<string>(
                name: "MarketName",
                table: "PunkzNfts",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketName",
                table: "PunkzNfts");

            migrationBuilder.RenameColumn(
                name: "MarketUrl",
                table: "PunkzNfts",
                newName: "MarketId");
        }
    }
}
