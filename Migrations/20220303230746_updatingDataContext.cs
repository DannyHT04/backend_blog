using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_blog.Migrations
{
    public partial class updatingDataContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "BlogInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "BlogInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "BlogInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "BlogInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "BlogInfo");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "BlogInfo");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "BlogInfo");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "BlogInfo");
        }
    }
}
