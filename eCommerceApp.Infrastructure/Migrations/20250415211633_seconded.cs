using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seconded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37273bd6-ab37-4b8a-8748-16f3e43e5b55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f459e0b7-0e41-47b3-83cd-3c913bd87907");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("381a93ae-a7ac-4ca7-ba4b-df3b00de3904"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("80cea846-9f63-455c-9bdf-912a3ec72f7e"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("c07a0791-1fcb-4908-ba77-e6c678b2d93f"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36c7ff33-ea6e-4fcd-b061-447c19ae13ea", null, "Admin", "ADMIN" },
                    { "7ba59070-a062-4fb6-a195-2d48e61ce89c", null, "User", "NAME" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("003304e8-5b55-42c3-b7ca-9e8c2ec80157"), "Cash" },
                    { new Guid("163ee24a-99f5-4c6f-bcfe-1a104cd1ad1f"), "PayPal" },
                    { new Guid("f0ff127e-b693-4679-865a-035a186c5e8a"), "Credit Card" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36c7ff33-ea6e-4fcd-b061-447c19ae13ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ba59070-a062-4fb6-a195-2d48e61ce89c");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("003304e8-5b55-42c3-b7ca-9e8c2ec80157"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("163ee24a-99f5-4c6f-bcfe-1a104cd1ad1f"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("f0ff127e-b693-4679-865a-035a186c5e8a"));

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37273bd6-ab37-4b8a-8748-16f3e43e5b55", null, "User", "NAME" },
                    { "f459e0b7-0e41-47b3-83cd-3c913bd87907", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("381a93ae-a7ac-4ca7-ba4b-df3b00de3904"), "PayPal" },
                    { new Guid("80cea846-9f63-455c-9bdf-912a3ec72f7e"), "Credit Card" },
                    { new Guid("c07a0791-1fcb-4908-ba77-e6c678b2d93f"), "Cash" }
                });
        }
    }
}
