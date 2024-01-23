using System.ComponentModel.DataAnnotations;

namespace Sudoku.Domain.Models;

public class AddSudokuUserModel
{
    [Required(AllowEmptyStrings = false)]
    public string Login { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}
