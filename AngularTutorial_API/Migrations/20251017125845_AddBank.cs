using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularTutorial_API.Migrations
{
    /// <inheritdoc />
    public partial class AddBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Banks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
