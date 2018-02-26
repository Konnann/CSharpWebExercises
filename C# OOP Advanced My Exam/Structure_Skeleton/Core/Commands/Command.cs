using System.Collections.Generic;

public abstract class Command : ICommand
{
    public Command(IList<string> args, DraftManager manager)
    {
        this.Arguments = args;
        this.Manager = manager;
    }

    public DraftManager Manager { get; set; }
    public IList<string> Arguments { get; }
    public abstract string Execute();
}

