using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreDL.Migrations
{
    public partial class ForeignKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItemProduct");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_ProductID",
                table: "LineItems",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Products_ProductID",
                table: "LineItems",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Products_ProductID",
                table: "LineItems");

            migrationBuilder.DropIndex(
                name: "IX_LineItems_ProductID",
                table: "LineItems");

            migrationBuilder.CreateTable(
                name: "LineItemProduct",
                columns: table => new
                {
                    LineItemsLineItemID = table.Column<int>(type: "integer", nullable: false),
                    ProductsProductID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItemProduct", x => new { x.LineItemsLineItemID, x.ProductsProductID });
                    table.ForeignKey(
                        name: "FK_LineItemProduct_LineItems_LineItemsLineItemID",
                        column: x => x.LineItemsLineItemID,
                        principalTable: "LineItems",
                        principalColumn: "LineItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItemProduct_Products_ProductsProductID",
                        column: x => x.ProductsProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineItemProduct_ProductsProductID",
                table: "LineItemProduct",
                column: "ProductsProductID");
        }
    }
}
