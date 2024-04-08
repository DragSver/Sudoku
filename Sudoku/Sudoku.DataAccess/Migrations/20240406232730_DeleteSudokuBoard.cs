using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sudoku.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSudokuBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SudokuBoards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
