public class EnergyRepository : IEnergyRepository
{


    public double EnergyStored { get; protected set; }
    public bool TakeEnergy(double energyNeeded)
    {
        bool canTake = this.EnergyStored - energyNeeded >= 0;
        //TODO change
        if (canTake)
        {
            this.EnergyStored -= energyNeeded;
        }

        return canTake;
    }

    public void StoreEnergy(double energy)
    {
        this.EnergyStored += energy;
    }
}

