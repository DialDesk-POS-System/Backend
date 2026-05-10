using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DialDesk.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixReturnNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returns_SaleItems_NewSaleItemId",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "WarrantyPeriod",
                table: "Models");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClaimDate",
                table: "Warranties",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "NewSaleItemId",
                table: "Returns",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_SaleItems_NewSaleItemId",
                table: "Returns",
                column: "NewSaleItemId",
                principalTable: "SaleItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returns_SaleItems_NewSaleItemId",
                table: "Returns");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClaimDate",
                table: "Warranties",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NewSaleItemId",
                table: "Returns",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarrantyPeriod",
                table: "Models",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_SaleItems_NewSaleItemId",
                table: "Returns",
                column: "NewSaleItemId",
                principalTable: "SaleItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
