using Sudoku.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sudoku.Domain.Models;

public class AddSudokuBoardModel
{
    [Required]
    public SudokuUser SudokuUser { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string SudokuBoardData { get;set; }
}
