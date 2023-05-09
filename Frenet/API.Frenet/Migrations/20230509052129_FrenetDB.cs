using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Frenet.Migrations
{
    public partial class FrenetDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerCEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientCEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipmentInvoiceValue = table.Column<double>(type: "float", nullable: false),
                    ShippingServiceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientCountry = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Carrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Msg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingPrice = table.Column<double>(type: "float", nullable: false),
                    OriginalDeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalShippingPrice = table.Column<double>(type: "float", nullable: false),
                    ShippingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShippingId",
                table: "Products",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_ShippingId",
                table: "Quotes",
                column: "ShippingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Shippings");
        }
    }
}
