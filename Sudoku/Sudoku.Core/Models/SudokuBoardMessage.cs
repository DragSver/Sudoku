using Sudoku.Domain.Models;

namespace Sudoku.Core.Models;

public class SudokuBoardMessage : RabbitMessage<CreateSudokuBoardModel>
{
    public SudokuBoardMessage(MessageAction action, CreateSudokuBoardModel model) : base(action, model) { }
}
