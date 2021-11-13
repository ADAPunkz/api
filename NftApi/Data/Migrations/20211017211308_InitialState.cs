using Microsoft.EntityFrameworkCore.Migrations;

namespace NftApi.Migrations
{
    public partial class InitialState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Traits",
                columns: table => new
                {
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traits", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "PunkzNfts",
                columns: table => new
                {
                    Edition = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BackgroundValue = table.Column<string>(type: "TEXT", nullable: true),
                    TypeValue = table.Column<string>(type: "TEXT", nullable: true),
                    MouthValue = table.Column<string>(type: "TEXT", nullable: true),
                    EyesValue = table.Column<string>(type: "TEXT", nullable: true),
                    ImplantNodesValue = table.Column<string>(type: "TEXT", nullable: true),
                    HeadValue = table.Column<string>(type: "TEXT", nullable: true),
                    AccessoriesValue = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Score = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Ipfs = table.Column<string>(type: "TEXT", nullable: true),
                    OnSale = table.Column<bool>(type: "INTEGER", nullable: false),
                    SalePrice = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PunkzNfts", x => x.Edition);
                    table.ForeignKey(
                        name: "FK_PunkzNfts_Traits_AccessoriesValue",
                        column: x => x.AccessoriesValue,
                        principalTable: "Traits",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunkzNfts_Traits_BackgroundValue",
                        column: x => x.BackgroundValue,
                        principalTable: "Traits",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunkzNfts_Traits_EyesValue",
                        column: x => x.EyesValue,
                        principalTable: "Traits",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunkzNfts_Traits_HeadValue",
                        column: x => x.HeadValue,
                        principalTable: "Traits",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunkzNfts_Traits_ImplantNodesValue",
                        column: x => x.ImplantNodesValue,
                        principalTable: "Traits",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunkzNfts_Traits_MouthValue",
                        column: x => x.MouthValue,
                        principalTable: "Traits",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunkzNfts_Traits_TypeValue",
                        column: x => x.TypeValue,
                        principalTable: "Traits",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PunkzNfts_AccessoriesValue",
                table: "PunkzNfts",
                column: "AccessoriesValue");

            migrationBuilder.CreateIndex(
                name: "IX_PunkzNfts_BackgroundValue",
                table: "PunkzNfts",
                column: "BackgroundValue");

            migrationBuilder.CreateIndex(
                name: "IX_PunkzNfts_EyesValue",
                table: "PunkzNfts",
                column: "EyesValue");

            migrationBuilder.CreateIndex(
                name: "IX_PunkzNfts_HeadValue",
                table: "PunkzNfts",
                column: "HeadValue");

            migrationBuilder.CreateIndex(
                name: "IX_PunkzNfts_ImplantNodesValue",
                table: "PunkzNfts",
                column: "ImplantNodesValue");

            migrationBuilder.CreateIndex(
                name: "IX_PunkzNfts_MouthValue",
                table: "PunkzNfts",
                column: "MouthValue");

            migrationBuilder.CreateIndex(
                name: "IX_PunkzNfts_TypeValue",
                table: "PunkzNfts",
                column: "TypeValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PunkzNfts");

            migrationBuilder.DropTable(
                name: "Traits");
        }
    }
}
