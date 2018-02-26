public class PressureProvider : Provider
{
    private const int EnergyOutputMultiplier = 2;
    private const int ExtraDurability = 300;
    public PressureProvider(int id, double energyOutput) : base(id, energyOutput)
    {
        this.EnergyOutput *= EnergyOutputMultiplier;
        this.Durability += ExtraDurability;
    }
}

