using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularTutorial_API.Migrations
{
    /// <inheritdoc />
    public partial class LittleUpdateinBB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Banks_BankID1",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_BankID1",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "BankID1",
                table: "Banks");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankID1",
                table: "Banks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_BankID1",
                table: "Banks",
                column: "BankID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Banks_BankID1",
                table: "Banks",
                column: "BankID1",
                principalTable: "Banks",
                principalColumn: "BankID");
        }
    }
}
