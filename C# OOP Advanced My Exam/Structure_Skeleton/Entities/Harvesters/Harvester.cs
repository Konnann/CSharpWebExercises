using System;
using System.CodeDom;

public abstract class Harvester : IHarvester
{
    private const int InitialDurability = 1000;
    private const int DurabilityLoss = 100;
    private int id;
    private double oreOutput;
    private double energyRequirement;
    private double durability;

    protected Harvester(int id, double oreOutput, double energyRequirement)
    {
        this.ID = id;
        this.OreOutput = oreOutput;
        this.EnergyRequirement = energyRequirement;
        this.Durability = InitialDurability;
    }

    public int ID {
        get { return this.id;  }
        protected set { this.id = value; }
    }

    public double OreOutput {
        get { return this.oreOutput; }
        protected set { this.oreOutput = value; }
    }

    public double EnergyRequirement {
        get { return this.energyRequirement; }
        protected set { this.energyRequirement = value; }
    }

    public virtual double Durability {
        get { return this.durability; }
        protected set { this.durability = value; }
    }

    public double Produce()
    {
        return this.oreOutput;
    }

    public void Broke()
    {
        Durability -= DurabilityLoss;

        if (this.Durability < 0)
        {
            throw new Exception();
        }
    }
}