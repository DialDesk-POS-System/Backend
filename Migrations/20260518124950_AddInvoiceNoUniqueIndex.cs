using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DialDesk.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceNoUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sales_InvoiceNo",
                table: "Sales",
                column: "InvoiceNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sales_InvoiceNo",
                table: "Sales");
        }
    }
}
