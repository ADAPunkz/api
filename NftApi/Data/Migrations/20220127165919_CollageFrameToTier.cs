using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    public partial class CollageFrameToTier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollageNfts_Trait_FrameValue",
                table: "CollageNfts");

            migrationBuilder.RenameColumn(
                name: "FrameValue",
                table: "CollageNfts",
                newName: "TierValue");

            migrationBuilder.RenameIndex(
                name: "IX_CollageNfts_FrameValue",
                table: "CollageNfts",
                newName: "IX_CollageNfts_TierValue");

            migrationBuilder.AddForeignKey(
                name: "FK_CollageNfts_Trait_TierValue",
                table: "CollageNfts",
                column: "TierValue",
                principalTable: "Trait",
                principalColumn: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollageNfts_Trait_TierValue",
                table: "CollageNfts");

            migrationBuilder.RenameColumn(
                name: "TierValue",
                table: "CollageNfts",
                newName: "FrameValue");

            migrationBuilder.RenameIndex(
                name: "IX_CollageNfts_TierValue",
                table: "CollageNfts",
                newName: "IX_CollageNfts_FrameValue");

            migrationBuilder.AddForeignKey(
                name: "FK_CollageNfts_Trait_FrameValue",
                table: "CollageNfts",
                column: "FrameValue",
                principalTable: "Trait",
                principalColumn: "Value");
        }
    }
}
