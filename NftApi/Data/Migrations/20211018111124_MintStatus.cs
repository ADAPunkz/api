using Microsoft.EntityFrameworkCore.Migrations;

namespace NftApi.Migrations
{
    public partial class MintStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Minted",
                table: "PunkzNfts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minted",
                table: "PunkzNfts");
        }
    }
}
