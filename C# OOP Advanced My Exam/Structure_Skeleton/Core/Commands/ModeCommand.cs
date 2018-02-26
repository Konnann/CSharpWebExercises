
using System.Collections.Generic;

public class ModeCommand : Command
{
    public ModeCommand(IList<string> args, DraftManager manager) : base(args, manager)
    {
    }

    public override string Execute()
    {
        return this.Manager.Mode(this.Arguments);
    }
}

