
using System;
using System.Collections.Generic;
using System.Linq;

class HarvesterController : IHarvesterController
{
    private List<IHarvester> harvesters;
    private IHarvesterFactory factory;

    public HarvesterController()
    {
        this.harvesters = new List<IHarvester>();
        this.factory = new HarvesterFactory();
    }

    public IList<IHarvester> Harvesters
    { get { return this.harvesters.AsReadOnly(); } }
    public string Register(IList<string> arguments)
    {
        var harvester = this.factory.GenerateHarvester(arguments);
        this.harvesters.Add(harvester);
        return string.Format(Constants.SuccessfullRegistration,
            harvester.GetType().Name);
    }

    public double OreProduced { get; protected set; }

    public string Produce()
    {

        return string.Format(Constants.OreOutputToday, -1);
    }

    public string ChangeMode(string mode)
    {
        List<IHarvester> dead = new List<IHarvester>();
        foreach (var harvester in this.harvesters)
        {
            try
            {
                harvester.Broke();
            }
            catch (Exception ex)
            {
                dead.Add(harvester);
            }
        }

        foreach (var entity in dead)
        {
            this.harvesters.Remove(entity);
        }

        return $"Mode changed to {mode}!";
    }
}

