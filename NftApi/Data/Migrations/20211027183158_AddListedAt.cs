using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NftApi.Migrations
{
    public partial class AddListedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ListedAt",
                table: "PunkzNfts",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListedAt",
                table: "PunkzNfts");
        }
    }
}
