public class SolarProvider : Provider
{
    private const int ExtraDurability = 500;
    public SolarProvider(int id, double energyOutput) : base(id, energyOutput)
    {
        this.Durability += ExtraDurability;
    }
}

