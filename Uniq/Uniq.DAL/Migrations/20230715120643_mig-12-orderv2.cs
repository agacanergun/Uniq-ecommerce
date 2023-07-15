using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig12orderv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SoldProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SoldProduct_ProductId",
                table: "SoldProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProduct_Product_ProductId",
                table: "SoldProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldProduct_Product_ProductId",
                table: "SoldProduct");

            migrationBuilder.DropIndex(
                name: "IX_SoldProduct_ProductId",
                table: "SoldProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SoldProduct");
        }
    }
}
