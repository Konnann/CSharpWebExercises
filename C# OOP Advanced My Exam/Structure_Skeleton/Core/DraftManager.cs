using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;

public class DraftManager
{
    private string mode;
    private double totalStoredEnergy;
    private double totalMinedOre;
    private Dictionary<string, IHarvester> harvestersById;
    private Dictionary<string, IProvider> providersById;
    private IEnergyRepository energyRepository;
    private IHarvesterController harvesterController;
    private IProviderController providerController;

    public DraftManager(IEnergyRepository energyRepository, IHarvesterController harvesterController, IProviderController providerController)
    {
        this.mode = "Full";
        this.totalMinedOre = 0;
        this.totalStoredEnergy = 0;
        this.harvestersById = new Dictionary<string, IHarvester>();
        this.providersById = new Dictionary<string, IProvider>();
        this.energyRepository = energyRepository;
        this.harvesterController = harvesterController;
        this.providerController = providerController;
    }

    public string RegisterHarvester(IList<string> arguments)
    {
        var type = arguments[0];
        var id = arguments[1];
        var oreOutput = double.Parse(arguments[2]);
        var energyRequirement = double.Parse(arguments[3]);
        var sonicFactor = 0;
        if (arguments.Count == 5)
        {
            sonicFactor = int.Parse(arguments[4]);
        }

        try
        {
            HarvesterFactory fac = new HarvesterFactory();
            IHarvester harvester = fac.GenerateHarvester(arguments);
            this.harvestersById.Add(id, harvester);
        }
        catch (ArgumentException e)
        {
            return e.Message;
        }


        return this.harvesterController.Register(arguments);
    }

    public string RegisterProvider(IList<string> arguments)
    {
        var type = arguments[0];
        var id = arguments[1];
        var energyOutput = int.Parse(arguments[2]);
        
        try
        {
            ProviderFactory fac = new ProviderFactory();
            IProvider provider = fac.GenerateProvider(arguments);
            this.providersById.Add(id, provider);
        }
        catch (ArgumentException e)
        {
            return e.Message;
        }
        
        return this.providerController.Register(arguments);
    }

    public string Day()
    {
        //calculate provided power for the day
        double producedPower = 0;
        foreach (var provider in this.providersById)
        {
            producedPower += provider.Value.EnergyOutput;
        }
        this.providerController.Produce();
        this.updateProviders();

        var toRemove = this.providersById.Where(pair => pair.Value.Durability < 0)
            .Select(pair => pair.Key)
            .ToList();

        foreach (var key in toRemove)
        {
            this.providersById.Remove(key);
        }

        //add to the total energy
        this.totalStoredEnergy += producedPower;


        //calculate needed energy
        double neededEnergy = 0;
        foreach (var harvester in this.harvestersById)
        {
            if (this.mode == "Full")
            {
                neededEnergy += harvester.Value.EnergyRequirement;
            }
            else if (this.mode == "Half")
            {
                neededEnergy += harvester.Value.EnergyRequirement * 50 / 100;
            }
            else if (this.mode == "Energy")
            {
                neededEnergy += harvester.Value.EnergyRequirement * 20 / 100;
            }
        }

        //check if we can mine
        double minedOres = 0;

        if (this.energyRepository.TakeEnergy(neededEnergy))
        {
            foreach (var harvester in this.harvestersById.Values)
            {
                minedOres += harvester.OreOutput;
                //this.harvesterController.Produce();
            }
        }
        // if (this.totalStoredEnergy >= neededEnergy)
        // {
        //     //mine
        //     this.totalStoredEnergy -= neededEnergy;
        //     foreach (var harvester in this.harvestersById)
        //     {
        //         minedOres += harvester.Value.OreOutput;
        //     }
        // }

        //take the mode in mind
        if (this.mode == "Energy")
        {
            minedOres = minedOres * 20 / 100;
        }
        else if (this.mode == "Half")
        {
            minedOres = minedOres * 50 / 100;
        }

        this.totalMinedOre += minedOres;

        var sb = new StringBuilder();

        // sb.AppendLine($"A day has passed.");
        sb.AppendLine($"Produced {producedPower} energy today!");
        sb.Append($"Produced {minedOres} ore today!");
        return sb.ToString().Trim();
    }

    public string Mode(IList<string> arguments)
    {
        this.mode = arguments[0];
        string result = this.harvesterController.ChangeMode(this.mode);

        var toRemove = this.harvestersById.Where(pair => pair.Value.Durability < 0)
            .Select(pair => pair.Key)
            .ToList();

        foreach (var key in toRemove)
        {
            this.harvestersById.Remove(key);
        }
        this.updateHarvesters();
        return result;
    }

    private void updateHarvesters()
    {
        var harvesters = (this.harvesterController as HarvesterController).Harvesters;
        foreach (var harv in harvesters)
        {
            for (int i = 0; i < this.harvestersById.Count; i++)
            {
                this.harvestersById[harv.ID.ToString()] = harv;
            }
        }
    }

    private void updateProviders()
    {
        Type type = typeof(ProviderController);

        FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        List<IProvider> providers = fields[0].GetValue(this.providerController) as List<IProvider>;

        foreach (var prov in providers)
        {
            for (int i = 0; i < this.harvestersById.Count; i++)
            {
                this.providersById[prov.ID.ToString()] = prov;
            }
        }
    }

    public string Check(IList<string> arguments)
    {
        var id = arguments[0];
        var sb = new StringBuilder();
        if (this.providersById.ContainsKey(id))
        {
            sb.AppendLine(providersById[id].ToString());
            sb.AppendLine($"Durability: {providersById[id].Durability}");
        }
        if (this.harvestersById.ContainsKey(id))
        {
            sb.AppendLine(harvestersById[id].ToString());
            sb.AppendLine($"Durability: {harvestersById[id].Durability}");
        }
        if (string.IsNullOrWhiteSpace(sb.ToString()))
        {
            sb.AppendLine($"No entity found with id - {id}");
        }

        return sb.ToString().Trim();
    }

    public string ShutDown()
    {
        var sb = new StringBuilder();
        sb.AppendLine("System Shutdown");
        sb.AppendLine($"Total Energy Produced: {this.totalStoredEnergy}");
        sb.Append($"Total Mined Plumbus Ore: {this.totalMinedOre}");

        return sb.ToString();
    }

    public string Repair(IList<string> args)
    {
        double value = double.Parse(args[0]);

        foreach (var p in providersById.Values)
        {
            p.Repair(value);
        }

        return $"Providers are repaired by {value}";
    }

    //public string Inspect(IList<string> args)
    //{
    //    string result = "";
    //
    //    string id = args[0];
    //
    //    if (harvestersById.ContainsKey(id))
    //    {
    //        result = harvestersById[id].ToString();
    //    }
    //    else if (providersById.ContainsKey(id))
    //    {
    //        result = providersById[id].ToString();
    //    }
    //    else
    //    {
    //        result = $"No entity found with id – {id}.";
    //    }
    //
    //    return result;
    //}
}
