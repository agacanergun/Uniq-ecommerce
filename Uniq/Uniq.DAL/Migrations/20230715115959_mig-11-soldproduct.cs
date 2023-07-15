using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig11soldproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SoldProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SoldProduct");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");
        }
    }
}
