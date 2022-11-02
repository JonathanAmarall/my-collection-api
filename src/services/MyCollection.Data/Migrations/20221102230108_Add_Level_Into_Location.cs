using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Data.Migrations
{
    public partial class Add_Level_Into_Location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Locations");
        }
    }
}
