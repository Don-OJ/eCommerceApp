using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aca3c2c-2c59-46b0-91e4-889615c67c06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eb047d4-bfe0-49a9-ab5b-137b2c69715c");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("342f2aa1-0f11-4067-bc61-2de79164fba0"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("8c08f2e7-f62c-4ce9-a8ab-e66800278da6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("b8ef9acf-e359-41c4-8c0c-de1692f157fd"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0aca3c2c-2c59-46b0-91e4-889615c67c06", null, "User", "NAME" },
                    { "0eb047d4-bfe0-49a9-ab5b-137b2c69715c", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("342f2aa1-0f11-4067-bc61-2de79164fba0"), "Credit Card" },
                    { new Guid("8c08f2e7-f62c-4ce9-a8ab-e66800278da6"), "PayPal" },
                    { new Guid("b8ef9acf-e359-41c4-8c0c-de1692f157fd"), "Cash" }
                });
        }
    }
}
