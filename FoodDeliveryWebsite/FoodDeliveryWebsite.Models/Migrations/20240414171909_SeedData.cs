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
                table: "product",
                columns: new[] { "id", "createdate", "creatoruserid", "description", "grams", "image", "imagemimetype", "imagename", "isdeleted", "name", "price", "status", "type" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 20, 13, 0, 0, 0, DateTimeKind.Utc), 1, "Бейби моцарела, паста коралини, чери домати, бекон, пармезан, микс зелени салати, млечен сос, чесън, магданозено песто и семена.", 350, null, null, null, false, "Италиана", 11.49m, 1, 1 },
                    { 2, new DateTime(2023, 12, 20, 13, 1, 0, 0, DateTimeKind.Utc), 1, "Разядка от авокадо, Пико де Гайо салца, халапеньо, фреш лайм, лук, кориандър и тортила чипс.", 180, null, null, null, false, "Домашно гуакамоле", 9.99m, 1, 2 },
                    { 3, new DateTime(2023, 12, 20, 13, 2, 0, 0, DateTimeKind.Utc), 1, "Пилешка пържола от бут в хрупкава панировка, със сос Холандез и печени картофи със зелен лук.", 450, null, null, null, false, "Хрупкаво пиле", 14.99m, 1, 3 },
                    { 4, new DateTime(2023, 12, 20, 13, 3, 0, 0, DateTimeKind.Utc), 1, "Хрупкави пикантни скариди с шрирача чили майонеза.", 150, null, null, null, false, "Хрупкави пикантни скариди", 16.49m, 1, 4 },
                    { 5, new DateTime(2023, 12, 20, 13, 4, 0, 0, DateTimeKind.Utc), 1, "Италиански хляб.", 80, null, null, null, false, "Чабата", 2.99m, 1, 5 },
                    { 6, new DateTime(2023, 12, 20, 13, 5, 0, 0, DateTimeKind.Utc), 1, "Торта от фин млечен шоколад и бисквитен блат, поръсена с какао.", 100, null, null, null, false, "Шоколадова торта с Линдт", 6.49m, 1, 6 },
                    { 7, new DateTime(2023, 12, 20, 13, 6, 0, 0, DateTimeKind.Utc), 1, "Крехки пилешки карета на скара с гарнитура картофено пюре, хрупкави краставички и сос блу чийз.", 220, null, null, null, false, "Детско меню с крехки карета", 9.49m, 1, 7 }
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

            migrationBuilder.InsertData(
                table: "order",
                columns: new[] { "id", "addressid", "createdate", "creatoruserid", "deliveryprice", "discountid", "totalprice", "userid" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 4, 1, 7, 35, 10, 0, DateTimeKind.Utc), 2, 7m, null, 56.46m, 2 },
                    { 2, 3, new DateTime(2024, 4, 2, 18, 25, 15, 0, DateTimeKind.Utc), 3, 7m, null, 32.96m, 3 },
                    { 3, 3, new DateTime(2024, 4, 3, 16, 23, 17, 0, DateTimeKind.Utc), 3, 7m, 3, 39.36m, 3 }
                });

            migrationBuilder.InsertData(
                table: "orderitem",
                columns: new[] { "id", "createdate", "creatoruserid", "orderid", "price", "productid", "productquantity", "userid" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 1, 7, 30, 10, 0, DateTimeKind.Utc), 0, 1, 16.49m, 4, 1, 2 },
                    { 2, new DateTime(2024, 4, 1, 7, 31, 10, 0, DateTimeKind.Utc), 2, 1, 22.98m, 1, 2, 2 },
                    { 3, new DateTime(2024, 4, 1, 7, 32, 10, 0, DateTimeKind.Utc), 2, 1, 9.99m, 2, 1, 2 },
                    { 4, new DateTime(2024, 4, 2, 17, 28, 11, 0, DateTimeKind.Utc), 3, 2, 25.96m, 6, 4, 3 },
                    { 5, new DateTime(2024, 4, 3, 16, 22, 11, 0, DateTimeKind.Utc), 3, 3, 29.98m, 3, 2, 3 },
                    { 6, new DateTime(2024, 4, 3, 16, 22, 17, 0, DateTimeKind.Utc), 3, 3, 5.98m, 5, 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "address",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "orderitem",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "orderitem",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "orderitem",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "orderitem",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "orderitem",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "orderitem",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "address",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "address",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 3);

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
