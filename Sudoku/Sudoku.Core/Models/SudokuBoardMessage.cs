namespace Sudoku.Core.Models;

public class SudokuBoardMessage : RabbitMessage<Guid>
{
    public SudokuBoardMessage(MessageAction action, Guid model) : base(action, model) { }
}
