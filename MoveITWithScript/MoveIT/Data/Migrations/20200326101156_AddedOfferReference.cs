using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoveIT.Data.Migrations
{
    public partial class AddedOfferReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelocationOfferReferences",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelocationOfferId = table.Column<int>(nullable: false),
                    Reference = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelocationOfferReferences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RelocationOfferReferences_RelocationOffers_RelocationOfferId",
                        column: x => x.RelocationOfferId,
                        principalTable: "RelocationOffers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelocationOfferReferences_RelocationOfferId",
                table: "RelocationOfferReferences",
                column: "RelocationOfferId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelocationOfferReferences");
        }
    }
}
