using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DialDesk.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Models",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Models");
        }
    }
}
