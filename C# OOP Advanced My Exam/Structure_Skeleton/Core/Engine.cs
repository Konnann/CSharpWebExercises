using System;
using System.Collections.Generic;
using System.Linq;

public class Engine
{
    private DraftManager manager;
    private ICommandInterpreter cmd;
    private IEnergyRepository energy;
    private IHarvesterController harvesterController;
    private IProviderController providerController;

    public Engine()
    {
        this.energy = new EnergyRepository();
        this.harvesterController = new HarvesterController();
        this.providerController = new ProviderController(energy);
        this.manager = new DraftManager(this.energy, this.harvesterController, this.providerController);
        this.cmd = new CommandInterpreter(this.harvesterController, this.providerController, this.manager);
    }

    public void Run()
    {
        while (true)
        {
            var input = Console.ReadLine().Trim();
            var data = input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var newData = new List<string>();
            foreach (var d in data)
            {
                if (d != null)
                {
                    newData.Add(d);
                }
            }
             Console.WriteLine(cmd.ProcessCommand(newData));
        }
    }
}


// var command = data[0];
//switch (command)
//{
//    case "RegisterHarvester":
//        var args = new List<string>(data.Skip(1).ToList());
//        manager.RegisterHarvester(args);
//        break;
//    case "RegisterProvider":
//        args = new List<string>(data.Skip(1).ToList());
//        manager.RegisterProvider(args);
//        break;
//    case "Day":
//        manager.Day();
//        break;
//    case "Mode":
//        args = new List<string>(data.Skip(1).ToList());
//        manager.Mode(args);
//        break;
//    case "Check":
//        args = new List<string>(data.Skip(1).ToList());
//        //Console.WriteLine(manager.Check(args));
//        break;
//    default:
//        manager.ShutDown();
//        Environment.Exit(0);
//        break;
//}
