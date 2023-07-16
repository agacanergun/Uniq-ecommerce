using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig14sliders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmallTitle = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    LeftPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmallSlider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstTitle = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    FirstDescription = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    SecondTitle = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    SecondDescription = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ThirdTitle = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ThirdDescription = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    FourthTitle = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    FourthDescription = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmallSlider", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "SmallSlider");
        }
    }
}
