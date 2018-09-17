// using Assets.Take_II.Scripts.PlayerManager;

// namespace Assets.Take_II.Scripts.Talent_Structures.Elemental_Talents
// {
//     public abstract class ElementalEmpowerTalent : ElementalTalentBase
//     {
//         public int EmpowerPoints { get; private set; }

//         protected bool Transmuted;
//         protected Enums.Elements TransmutedElement;
//         protected Enums.Elements OriginalElement;

//         protected ElementalEmpowerTalent(string name, string descrpition, Enums.Elements element, int empowerPoints) : base(name, descrpition, element)
//         {
//             EmpowerPoints = empowerPoints;
//         }

//         public override void ApplyEffect(Player player)
//         {
//              player.TalentTree.AvailableEmpowerPoints[Element] += EmpowerPoints;
//         }
//     }
// }