using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Domain.Models;

public class AddSudokuUserModel
{
    [Required(AllowEmptyStrings = false)]
    public string Login { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}
