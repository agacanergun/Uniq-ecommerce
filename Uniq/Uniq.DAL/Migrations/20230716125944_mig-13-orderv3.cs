using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig13orderv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAddressId",
                table: "Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDateTime",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDateTime",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "CustomerAddressId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
