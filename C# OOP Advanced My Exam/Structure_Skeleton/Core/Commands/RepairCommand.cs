
using System.Collections.Generic;

public class RepairCommand : Command
{
    private List<string> args;

    public RepairCommand(IList<string> args, DraftManager manager) : base(args, manager)
    {}

    public override string Execute()
    {
       return this.Manager.Repair(this.Arguments);
    }
}

