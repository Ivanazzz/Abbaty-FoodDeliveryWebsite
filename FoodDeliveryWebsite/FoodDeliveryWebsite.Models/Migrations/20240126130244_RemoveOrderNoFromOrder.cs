using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrderNoFromOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderno",
                table: "order");

            migrationBuilder.AddColumn<decimal>(
                name: "deliveryprice",
                table: "order",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "totalprice",
                table: "order",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deliveryprice",
                table: "order");

            migrationBuilder.DropColumn(
                name: "totalprice",
                table: "order");

            migrationBuilder.AddColumn<int>(
                name: "orderno",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
