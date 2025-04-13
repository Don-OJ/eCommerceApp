using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df7ef22f-eadf-4ebe-802f-269446280606");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e72ed35d-51a0-4268-83fa-a72959ad95b4");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("5cee7ce2-92fe-4e0e-9def-9936e7baaf3b"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("6c8a265e-da75-4550-ba18-654028f1f0cc"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("8b708727-fdbe-40c0-8cc3-23e65948d1a9"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CheckOutArchives",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CheckOutArchives",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "df7ef22f-eadf-4ebe-802f-269446280606", null, "Admin", "ADMIN" },
                    { "e72ed35d-51a0-4268-83fa-a72959ad95b4", null, "User", "NAME" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5cee7ce2-92fe-4e0e-9def-9936e7baaf3b"), "PayPal" },
                    { new Guid("6c8a265e-da75-4550-ba18-654028f1f0cc"), "Credit Card" },
                    { new Guid("8b708727-fdbe-40c0-8cc3-23e65948d1a9"), "Cash" }
                });
        }
    }
}
