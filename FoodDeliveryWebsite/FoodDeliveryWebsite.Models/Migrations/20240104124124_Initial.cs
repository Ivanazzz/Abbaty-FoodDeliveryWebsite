using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    streetno = table.Column<int>(type: "integer", nullable: false),
                    floor = table.Column<int>(type: "integer", nullable: true),
                    apartmentno = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "discount",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    percentage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discount", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    phonenumber = table.Column<string>(type: "text", nullable: false),
                    addressid = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_address_addressid",
                        column: x => x.addressid,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    orderno = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    discountid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_discount_discountid",
                        column: x => x.discountid,
                        principalTable: "discount",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_order_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    grams = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<byte[]>(type: "bytea", nullable: false),
                    orderid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_order_orderid",
                        column: x => x.orderid,
                        principalTable: "order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_discountid",
                table: "order",
                column: "discountid");

            migrationBuilder.CreateIndex(
                name: "IX_order_userid",
                table: "order",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_product_orderid",
                table: "product",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_user_addressid",
                table: "user",
                column: "addressid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "discount");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "address");
        }
    }
}
