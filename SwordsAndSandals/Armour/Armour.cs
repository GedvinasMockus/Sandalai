using SwordsAndSandals.Stats;

namespace SwordsAndSandals.Armour;

public abstract class Armour
{
    protected MetalType metalType;

    public Armour(MetalType metalType)
    {
        this.metalType = metalType;
    }

    public abstract Attributes EquipArmour();
}