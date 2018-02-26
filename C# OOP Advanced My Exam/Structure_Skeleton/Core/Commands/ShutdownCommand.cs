using System.Collections.Generic;

public class ShutdownCommand : Command
{

    public ShutdownCommand(IList<string> args, DraftManager manager) : base(args, manager)
    {
    }

    public override string Execute()
    {
        return this.Manager.ShutDown();
    }
}