using SwordsAndSandals.Stats;

namespace SwordsAndSandals.Armour;

public class Helmet : Armour
{
    public Helmet(MetalType metalType) : base(metalType)
    {

    }

    public override Attributes EquipArmour()
    {
        return new Attributes() { ArmourRating = 5 + metalType.AddArmourRating() };
    }
}