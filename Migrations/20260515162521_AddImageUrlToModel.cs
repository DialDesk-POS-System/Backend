using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DialDesk.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageryUrl",
                table: "Models",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageryUrl",
                table: "Models");
        }
    }
}
