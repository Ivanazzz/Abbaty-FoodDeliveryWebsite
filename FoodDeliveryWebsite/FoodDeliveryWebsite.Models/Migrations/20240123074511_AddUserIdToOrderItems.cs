using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitem_user_userid",
                table: "orderitem");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "orderitem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orderitem_user_userid",
                table: "orderitem",
                column: "userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitem_user_userid",
                table: "orderitem");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "orderitem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_orderitem_user_userid",
                table: "orderitem",
                column: "userid",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
