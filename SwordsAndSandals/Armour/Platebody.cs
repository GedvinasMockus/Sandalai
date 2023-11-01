using SwordsAndSandals.Stats;

namespace SwordsAndSandals.Armour;

public class Platebody : Armour
{
    public Platebody(MetalType metalType) : base(metalType)
    {

    }

    public override Attributes EquipArmour()
    {
        return new Attributes() { ArmourRating = 20 + metalType.AddArmourRating() };
    }
}