using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Domain.Response;

public class GetSudokuUserResponse
{
    public string Login { get; set; }
    public string Password { get; set; }
}
