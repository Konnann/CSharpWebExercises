using System;

public abstract class Provider : IProvider
{
    private const int InitialDurability = 1000;
    private const int DurabilityLoss = 100;
    public Provider(int id, double energyOutput)
    {
        this.ID = id;
        this.EnergyOutput = energyOutput;
        this.Durability = InitialDurability;
    }

    public int ID { get; }
    public double Durability { get; protected set; }
    public double EnergyOutput { get; protected set; }

    public void IncreaseDurability(double value)
    {
        this.Durability += value;
    }


    public double Produce()
    {
        return this.EnergyOutput;
    }

    public void Broke()
    {
        Durability -= DurabilityLoss;

        if (this.Durability < 0)
        {
            throw new Exception();
        }
    }

    public void Repair(double val)
    {
        Durability += val;
    }
}