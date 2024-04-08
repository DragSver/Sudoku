using System.ComponentModel.DataAnnotations;

namespace Sudoku.Domain.Models.SudokuBoardsModels;

public class DeleteFavoriteSudokuBoardModel
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid SudokuBoardId { get; set; }
}
