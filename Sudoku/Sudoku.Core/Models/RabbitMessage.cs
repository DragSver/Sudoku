namespace Sudoku.Core.Models;

public abstract class RabbitMessage<T>
{
    public MessageAction Action { get; set; }
    public T MessageModel { get; set; }

    public RabbitMessage(MessageAction action, T model) {
        Action = action;
        MessageModel = model;
    }
}
