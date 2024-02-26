using System.ComponentModel.DataAnnotations;

namespace Sudoku.Domain.Models;

public class SudokuUserModel
{
    [Required(AllowEmptyStrings = false)]
    public string Login { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}
