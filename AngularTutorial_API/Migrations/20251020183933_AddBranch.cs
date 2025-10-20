using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularTutorial_API.Migrations
{
    /// <inheritdoc />
    public partial class AddBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankID1",
                table: "Banks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                    table.ForeignKey(
                        name: "FK_Branches_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "BankID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_BankID1",
                table: "Banks",
                column: "BankID1");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_BankID",
                table: "Branches",
                column: "BankID");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Banks_BankID1",
                table: "Banks",
                column: "BankID1",
                principalTable: "Banks",
                principalColumn: "BankID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Banks_BankID1",
                table: "Banks");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Banks_BankID1",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "BankID1",
                table: "Banks");
        }
    }
}
