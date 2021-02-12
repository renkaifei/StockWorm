using StockWorm.Domain.Interfaces;
using System.Collections.Generic;
public class MarcoCommand:ICommand
{
    private List<ICommand> commands;
    public MarcoCommand()
    {
        commands = new List<ICommand>();
    }

    public void Add(ICommand command)
    {
        commands.Add(command);
    }

    public void Execute()
    {
        foreach(ICommand command in commands)
        {
            command.Execute();
        }
    }
}