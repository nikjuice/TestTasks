using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoveIT.Data.Migrations
{
    public partial class AddedRelocationOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    AddressLine = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RelocationPriceInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalGrossPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelocationPriceInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RelocationInquiry",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AddressFromID = table.Column<int>(nullable: true),
                    AddressToID = table.Column<int>(nullable: true),
                    Distance = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    SpecialArea = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelocationInquiry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RelocationInquiry_Address_AddressFromID",
                        column: x => x.AddressFromID,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelocationInquiry_Address_AddressToID",
                        column: x => x.AddressToID,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelocationPricePart",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePart = table.Column<decimal>(nullable: false),
                    PriceDesription = table.Column<string>(nullable: true),
                    RelocationPriceInfoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelocationPricePart", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RelocationPricePart_RelocationPriceInfo_RelocationPriceInfoID",
                        column: x => x.RelocationPriceInfoID,
                        principalTable: "RelocationPriceInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelocationOffers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    InquiryID = table.Column<int>(nullable: true),
                    PriceInfoID = table.Column<int>(nullable: true),
                    PlacedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelocationOffers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RelocationOffers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelocationOffers_RelocationInquiry_InquiryID",
                        column: x => x.InquiryID,
                        principalTable: "RelocationInquiry",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelocationOffers_RelocationPriceInfo_PriceInfoID",
                        column: x => x.PriceInfoID,
                        principalTable: "RelocationPriceInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelocationInquiry_AddressFromID",
                table: "RelocationInquiry",
                column: "AddressFromID");

            migrationBuilder.CreateIndex(
                name: "IX_RelocationInquiry_AddressToID",
                table: "RelocationInquiry",
                column: "AddressToID");

            migrationBuilder.CreateIndex(
                name: "IX_RelocationOffers_ApplicationUserId",
                table: "RelocationOffers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RelocationOffers_InquiryID",
                table: "RelocationOffers",
                column: "InquiryID");

            migrationBuilder.CreateIndex(
                name: "IX_RelocationOffers_PriceInfoID",
                table: "RelocationOffers",
                column: "PriceInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_RelocationPricePart_RelocationPriceInfoID",
                table: "RelocationPricePart",
                column: "RelocationPriceInfoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelocationOffers");

            migrationBuilder.DropTable(
                name: "RelocationPricePart");

            migrationBuilder.DropTable(
                name: "RelocationInquiry");

            migrationBuilder.DropTable(
                name: "RelocationPriceInfo");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
