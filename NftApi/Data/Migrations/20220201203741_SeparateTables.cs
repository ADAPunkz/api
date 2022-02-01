using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    public partial class SeparateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollageNfts_Trait_TierValue",
                table: "CollageNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_CollageNfts_Trait_TypeValue",
                table: "CollageNfts");

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
                name: "Trait");

            migrationBuilder.CreateTable(
                name: "CollageTraits",
                columns: table => new
                {
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollageTraits", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "PunkzTraits",
                columns: table => new
                {
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PunkzTraits", x => x.Value);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CollageNfts_CollageTraits_TierValue",
                table: "CollageNfts",
                column: "TierValue",
                principalTable: "CollageTraits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_CollageNfts_CollageTraits_TypeValue",
                table: "CollageNfts",
                column: "TypeValue",
                principalTable: "CollageTraits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_AccessoriesValue",
                table: "PunkzNfts",
                column: "AccessoriesValue",
                principalTable: "PunkzTraits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_BackgroundValue",
                table: "PunkzNfts",
                column: "BackgroundValue",
                principalTable: "PunkzTraits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_EyesValue",
                table: "PunkzNfts",
                column: "EyesValue",
                principalTable: "PunkzTraits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_HeadValue",
                table: "PunkzNfts",
                column: "HeadValue",
                principalTable: "PunkzTraits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_ImplantNodesValue",
                table: "PunkzNfts",
                column: "ImplantNodesValue",
                principalTable: "PunkzTraits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_MouthValue",
                table: "PunkzNfts",
                column: "MouthValue",
                principalTable: "PunkzTraits",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_TypeValue",
                table: "PunkzNfts",
                column: "TypeValue",
                principalTable: "PunkzTraits",
                principalColumn: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollageNfts_CollageTraits_TierValue",
                table: "CollageNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_CollageNfts_CollageTraits_TypeValue",
                table: "CollageNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_AccessoriesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_BackgroundValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_EyesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_HeadValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_ImplantNodesValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_MouthValue",
                table: "PunkzNfts");

            migrationBuilder.DropForeignKey(
                name: "FK_PunkzNfts_PunkzTraits_TypeValue",
                table: "PunkzNfts");

            migrationBuilder.DropTable(
                name: "CollageTraits");

            migrationBuilder.DropTable(
                name: "PunkzTraits");

            migrationBuilder.CreateTable(
                name: "Trait",
                columns: table => new
                {
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trait", x => x.Value);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CollageNfts_Trait_TierValue",
                table: "CollageNfts",
                column: "TierValue",
                principalTable: "Trait",
                principalColumn: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_CollageNfts_Trait_TypeValue",
                table: "CollageNfts",
                column: "TypeValue",
                principalTable: "Trait",
                principalColumn: "Value");

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
    }
}
