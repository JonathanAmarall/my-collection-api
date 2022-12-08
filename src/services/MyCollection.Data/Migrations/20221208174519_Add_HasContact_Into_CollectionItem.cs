using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Data.Migrations
{
    public partial class Add_HasContact_Into_CollectionItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasContact",
                table: "CollectionItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasContact",
                table: "CollectionItems");
        }
    }
}
