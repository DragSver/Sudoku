using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sudoku.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateSudokuBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SudokuBoards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SudokuBoardModelJson = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SudokuBoards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SudokuBoards_SudokuUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SudokuUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SudokuBoards_UserId",
                table: "SudokuBoards",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SudokuBoards");
        }
    }
}
