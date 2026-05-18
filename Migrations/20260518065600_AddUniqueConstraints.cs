using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DialDesk.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Watches_SerialNo",
                table: "Watches",
                column: "SerialNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_ModelName",
                table: "Models",
                column: "ModelName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_ModelNo",
                table: "Models",
                column: "ModelNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Watches_SerialNo",
                table: "Watches");

            migrationBuilder.DropIndex(
                name: "IX_Models_ModelName",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_ModelNo",
                table: "Models");
        }
    }
}
