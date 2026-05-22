using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DialDesk.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddWarrantyStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Watches_WatchId",
                table: "Warranties");

            migrationBuilder.DropIndex(
                name: "IX_Warranties_WatchId",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "WatchId",
                table: "Warranties");

            migrationBuilder.AddColumn<int>(
                name: "WarrantyId",
                table: "Watches",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClaimed",
                table: "Warranties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Watches_WarrantyId",
                table: "Watches",
                column: "WarrantyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Watches_Warranties_WarrantyId",
                table: "Watches",
                column: "WarrantyId",
                principalTable: "Warranties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Watches_Warranties_WarrantyId",
                table: "Watches");

            migrationBuilder.DropIndex(
                name: "IX_Watches_WarrantyId",
                table: "Watches");

            migrationBuilder.DropColumn(
                name: "WarrantyId",
                table: "Watches");

            migrationBuilder.DropColumn(
                name: "IsClaimed",
                table: "Warranties");

            migrationBuilder.AddColumn<Guid>(
                name: "WatchId",
                table: "Warranties",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_WatchId",
                table: "Warranties",
                column: "WatchId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Watches_WatchId",
                table: "Warranties",
                column: "WatchId",
                principalTable: "Watches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
