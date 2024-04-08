using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "discount",
                columns: new[] { "id", "code", "createdate", "creatoruserid", "expirationdate", "percentage", "startdate" },
                values: new object[,]
                {
                    { 1, "year2024", new DateTime(2023, 12, 15, 17, 21, 35, 0, DateTimeKind.Utc), 1, new DateTime(2024, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), 24, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "summer20", new DateTime(2023, 12, 17, 2, 5, 47, 0, DateTimeKind.Utc), 1, new DateTime(2024, 8, 31, 23, 59, 59, 0, DateTimeKind.Utc), 20, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "april10", new DateTime(2023, 12, 18, 2, 5, 47, 0, DateTimeKind.Utc), 1, new DateTime(2024, 4, 30, 23, 59, 59, 0, DateTimeKind.Utc), 10, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "createdate", "creatoruserid", "email", "firstname", "gender", "isdeleted", "lastname", "password", "phonenumber", "role", "salt" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 8, 20, 50, 0, DateTimeKind.Utc), 0, "admin@gmail.com", "Админ", 1, false, "Админ", "41C396CABA416A55EF199EEF5BF18F2485378DD373764A50662B0BE863A218C107E5E3A7305A83A4EBD8A4F7493A79878A8F25F69057896D2CA1477214B31754", "+359 99 9999 999", 1, "8EB89F1556CB153CC0D048D70D3B59E03046A1B33EE056350BC3B965617FF54E6EE89B8E64DE705A05B49CDB80B4888BEF78DA7E4FD47FD20AC1EEADD238C8CE" },
                    { 2, new DateTime(2024, 1, 13, 14, 4, 39, 0, DateTimeKind.Utc), 0, "ivan@gmail.com", "Иван", 1, false, "Иванов", "5E79B341F17FCA086417D1CDBB0419F3F5ADD01D0908A0B331BA5BED403209DADD32DFD8533C56BF2394492D8923DC0B5ED6504803344AA804971B234DA294A8", "+359 88 8888 888", 2, "C0BFA2CEC43F2B8D90308BB9433B5DE0D5E74B36866DB81253F588AF8ED2815BC801CAF40EB2E4B10A1775D874602097B7358A8E10ABD9C35BEB87A0DFFAD3C6" },
                    { 3, new DateTime(2024, 2, 14, 10, 21, 18, 0, DateTimeKind.Utc), 0, "maria@abv.bg", "Мария", 2, false, "Петрова", "371E67B6847BA48915A2E37C3E1E98F3D8EEFE024EA0CD9C2DAA328743D3E4A87D5F746B80719F249C91311026438CFDA0A83718FEF8E0ED63924334325B8632", "+359 77 7777 777", 2, "2F785CBCB2BE890D21DA615BEC80A0C3B76C6DD8DC5621F5B7F12A6C668E2883819B470D4603B83295AC5BCBF7393394487199C28484051659B74EA71F3FA26E" }
                });

            migrationBuilder.InsertData(
                table: "address",
                columns: new[] { "id", "apartmentno", "city", "createdate", "creatoruserid", "floor", "isdeleted", "street", "streetno", "userid" },
                values: new object[,]
                {
                    { 1, null, "София", new DateTime(2024, 1, 14, 11, 26, 26, 0, DateTimeKind.Utc), 2, 2, false, "Витоша", 91, 2 },
                    { 2, 6, "Банкя", new DateTime(2024, 3, 3, 12, 12, 12, 0, DateTimeKind.Utc), 2, 5, false, "Христо Ботев", 28, 2 },
                    { 3, null, "Бургас", new DateTime(2024, 4, 1, 21, 18, 17, 0, DateTimeKind.Utc), 3, null, false, "Славянска", 13, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "address",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "address",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "address",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
