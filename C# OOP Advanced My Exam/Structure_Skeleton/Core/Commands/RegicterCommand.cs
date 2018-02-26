using System.Collections.Generic;
using System.Linq;

public class RegisterCommand: Command
{
    public RegisterCommand(IList<string> args, DraftManager manager) : base(args, manager)
    {}

    public override string Execute()
    {
        string result = "";

        if (this.Arguments[0] == "Harvester")
        {
            result = this.Manager.RegisterHarvester(new List<string>(this.Arguments.Skip(1).ToList()));
        }
        else if(this.Arguments[0] == "Provider")
        {
            result = this.Manager.RegisterProvider(new List<string>(this.Arguments.Skip(1).ToList()));
        }

        return result;
    }
}

