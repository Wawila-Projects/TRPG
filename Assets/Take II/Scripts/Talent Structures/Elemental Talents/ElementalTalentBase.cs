using System.Linq;
using Assets.Take_II.Scripts.PlayerManager;

namespace Assets.Take_II.Scripts.Talent_Structures.Elemental_Talents
{
    public abstract class ElementalTalentBase: TalentBase
    {
        protected Enums.Elements Element;

        protected ElementalTalentBase(string name, string description, Enums.Elements element) : base(name, description, element)
        {
            Element = element;
        }

        public override bool ActivateTalent()
        {
            if (IsActive || TalentTree.AvailableTalentPoints <= 0)
                return false;

            if (Requierments.Any(requierment => requierment.IsActive == false))
                return false;

            IsActive = true;
            ApplyEffect(TalentTree.Player);
            TalentTree.AvailableTalentPoints--;

            return true;
        }
       
        public abstract void ApplyEffect(Player player);
    }
}