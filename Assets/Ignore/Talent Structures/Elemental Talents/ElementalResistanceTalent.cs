// using Assets.Take_II.Scripts.PlayerManager;

// namespace Assets.Take_II.Scripts.Talent_Structures.Elemental_Talents
// {
//     public abstract class ElementalResistanceTalent : ElementalTalentBase
//     {
//         public int Resistance { get; private set; }

//         protected ElementalResistanceTalent(string name, string descrpition, Enums.Elements element, int resistance) : base(name, descrpition, element)
//         {
//             Resistance = resistance;
//         }

//         public override void ApplyEffect(Character player)
//         {
//             player.Resistances.ModifyResistance(Element, Resistance);
//         }
//     }
// }