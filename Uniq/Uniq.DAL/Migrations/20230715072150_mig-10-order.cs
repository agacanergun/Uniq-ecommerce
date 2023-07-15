using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig10order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoldProductId",
                table: "ProductPicture",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    CustomerAddressId = table.Column<int>(type: "int", nullable: false),
                    CustomerAdressesId = table.Column<int>(type: "int", nullable: true),
                    OrderStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    ShippingType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_CustomerAdresses_CustomerAdressesId",
                        column: x => x.CustomerAdressesId,
                        principalTable: "CustomerAdresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoldProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoldProduct_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPicture_SoldProductId",
                table: "ProductPicture",
                column: "SoldProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerAdressesId",
                table: "Order",
                column: "CustomerAdressesId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "OrderNumberUnique",
                table: "Order",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SoldProduct_OrderId",
                table: "SoldProduct",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPicture_SoldProduct_SoldProductId",
                table: "ProductPicture",
                column: "SoldProductId",
                principalTable: "SoldProduct",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPicture_SoldProduct_SoldProductId",
                table: "ProductPicture");

            migrationBuilder.DropTable(
                name: "SoldProduct");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_ProductPicture_SoldProductId",
                table: "ProductPicture");

            migrationBuilder.DropColumn(
                name: "SoldProductId",
                table: "ProductPicture");
        }
    }
}
