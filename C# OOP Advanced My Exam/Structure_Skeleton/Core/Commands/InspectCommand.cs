using System.Collections.Generic;

public class InspectCommand : Command
{
    public InspectCommand(IList<string> args, DraftManager manager) : base(args, manager)
    {
    }

    public override string Execute()
    {
        return this.Manager.Check(Arguments);
    }
}

