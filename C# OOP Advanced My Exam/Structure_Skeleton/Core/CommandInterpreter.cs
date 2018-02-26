using System;
using System.Collections.Generic;
using System.Linq;

class CommandInterpreter : ICommandInterpreter
{
    private DraftManager manager;

    public CommandInterpreter(IHarvesterController harvesterController, IProviderController providerController, DraftManager manager)
    {
        this.manager = manager;
        this.HarvesterController = harvesterController;
        this.ProviderController = providerController;
    }
    public IHarvesterController HarvesterController { get; }
    public IProviderController ProviderController { get; }

    public string ProcessCommand(IList<string> data)
    {
        ICommand command = null;
        var commandInfo = data[0];
        string result = "";


        switch (commandInfo)
        {
            case "Register":
                var args = new List<string>(data.Skip(1).ToList());
                command = new RegisterCommand(args, manager);
                break;
            case "Day":
                command = new DayCommand(null, manager);
                break;
            case "Mode":
                args = new List<string>(data.Skip(1).ToList());
                command = new ModeCommand(args, manager);
                break;
            case "Inspect":
                args = new List<string>(data.Skip(1).ToList());
                command = new InspectCommand(args, manager);
                //Console.WriteLine(manager.Check(args));
                break;
            case "Repair":
                {
                    args = new List<string>(data.Skip(1).ToList());
                    command = new RepairCommand(args, manager);
                    break;
                }
            case "Shutdown":

                command = new ShutdownCommand(null, manager);
                Console.WriteLine(command.Execute());
                Environment.Exit(0);
                break;
        }

        return command.Execute();
    }
}

