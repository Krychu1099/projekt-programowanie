using Microsoft.EntityFrameworkCore.Migrations;

namespace Calories.Migrations
{
    public partial class calories_model_result : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CalorieRequirement",
                table: "Calories",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalorieRequirement",
                table: "Calories");
        }
    }
}
