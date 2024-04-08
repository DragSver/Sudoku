using System.ComponentModel.DataAnnotations;

namespace Sudoku.Domain.Models.SudokuBoardsModels;

public class AddFavoriteSudokuBoardModel
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid SudokuBoardId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public SudokuBoardModel SudokuBoardModel { get; set; }
}
