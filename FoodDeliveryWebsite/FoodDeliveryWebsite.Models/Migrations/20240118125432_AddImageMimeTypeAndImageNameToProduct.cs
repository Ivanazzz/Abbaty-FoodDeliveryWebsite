using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddImageMimeTypeAndImageNameToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagemimetype",
                table: "product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imagename",
                table: "product",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagemimetype",
                table: "product");

            migrationBuilder.DropColumn(
                name: "imagename",
                table: "product");
        }
    }
}
