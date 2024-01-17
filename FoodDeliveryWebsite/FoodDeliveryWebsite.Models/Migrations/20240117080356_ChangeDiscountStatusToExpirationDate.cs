using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDiscountStatusToExpirationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "discount");

            migrationBuilder.AddColumn<DateTime>(
                name: "expirationdate",
                table: "discount",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expirationdate",
                table: "discount");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "discount",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
