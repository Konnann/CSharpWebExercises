
using System.Collections.Generic;

public class DayCommand : Command
{
    public DayCommand(IList<string> args, DraftManager manager) : base(args, manager)
    {
    }

    public override string Execute()
    {
        return this.Manager.Day();
    }
}

