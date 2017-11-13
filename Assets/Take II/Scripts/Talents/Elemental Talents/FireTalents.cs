using Assets.Take_II.Scripts.PlayerManager;
using Assets.Take_II.Scripts.Talent_Structures.Elemental_Talents;

namespace Assets.Take_II.Scripts.Talents.Elemental_Talents
{
    // TODO Add Descriptions
    public class FireInitialTalent : ElementalTalentBase
    {
        public FireInitialTalent() : base("Initial Fire Talent", "", Enums.Elements.Fire)
        {
        }

        public override void ApplyEffect(Player player)
        {
            TalentTree.AvailableEmpowerPoints[Element]++;
            TalentTree.Player.Resistances.ModifyResistance(Element, 2);
        }
    }

    public class FireResistance1 : ElementalResistanceTalent
    {
        public FireResistance1() : base("Fire Resistance 1","", Enums.Elements.Fire, 2)
        {
            Requierments.Add(TalentTree.Talents[typeof(FireInitialTalent).Name]);
        }
    }

    public class FireResistance2 : ElementalResistanceTalent
    {
        public FireResistance2() : base("Fire Resistance 2", "", Enums.Elements.Fire, 3)
        {
            Requierments.Add(TalentTree.Talents[typeof(FireInitialTalent).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance1).Name]);
        }
    }

    public class FireResistance3 : ElementalResistanceTalent
    {
        public FireResistance3() : base("Fire Resistance 3", "", Enums.Elements.Fire, 5)
        {
            Requierments.Add(TalentTree.Talents[typeof(FireInitialTalent).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance1).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance2).Name]);
        }
    }

    public class FireResistance4 : ElementalResistanceTalent
    {
        public FireResistance4() : base("Fire Resistance 4", "", Enums.Elements.Fire, 10)
        {
            Requierments.Add(TalentTree.Talents[typeof(FireInitialTalent).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance1).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance2).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance3).Name]);
        }
    }

    public class FireResistance5 : ElementalResistanceTalent
    {
        public FireResistance5() : base("Fire Resistance 5", "", Enums.Elements.Fire, 30)
        {
            Requierments.Add(TalentTree.Talents[typeof(FireInitialTalent).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance1).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance2).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance3).Name]);
            Requierments.Add(TalentTree.Talents[typeof(FireResistance4).Name]);
        }
    }
}