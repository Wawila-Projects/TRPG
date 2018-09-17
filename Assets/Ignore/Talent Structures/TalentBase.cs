// using System.Collections.Generic;
// using System.Linq;
// using Assets.Take_II.Scripts.PlayerManager;

// namespace Assets.Take_II.Scripts.Talent_Structures
// { 
//     public abstract class TalentBase
//     {
//         public string Name { get; protected set; }
//         public string Description { get; protected set; }
//         protected string IdName;

//         public TalentTree TalentTree;
//         protected List<TalentBase> Requierments;
//         public bool IsActive { get; protected set; }

//         public abstract bool ActivateTalent();

//         protected TalentBase(string name, string description, Enums.Elements element = Enums.Elements.None)
//         {
//             Requierments = new List<TalentBase>();
//             IdName = GetType().Name;
//             Name = name;
//             Description = description;
//             IsActive = false;
//             TalentTree.Talents.Add(IdName, this);

//             TalentManager.Talents[element].Add(IdName);   
//         }

//     }
// }