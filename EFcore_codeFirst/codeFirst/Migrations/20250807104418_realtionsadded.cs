using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace codeFirst.Migrations
{
    /// <inheritdoc />
    public partial class realtionsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarksId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_MarksId",
                table: "Students",
                column: "MarksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Marks_MarksId",
                table: "Students",
                column: "MarksId",
                principalTable: "Marks",
                principalColumn: "MarksId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Marks_MarksId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MarksId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MarksId",
                table: "Students");
        }
    }
}
