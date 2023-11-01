using SwordsAndSandals.Stats;

namespace SwordsAndSandals.Armour;

public class Platelegs : Armour
{
    public Platelegs(MetalType metalType) : base(metalType)
    {

    }

    public override Attributes EquipArmour()
    {
        return new Attributes() { ArmourRating = 10 + metalType.AddArmourRating() };
    }
}