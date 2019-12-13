using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills;
using Assets.Enums;

namespace Assets.Spells {
    public abstract class SupportSpell : CastableSpell {
        public override bool IsMagical => true;
        public abstract IList<PassiveSkillsBase> Effects { get; }
        protected override string Id => $"SupportSpell_${Name}";
    }

    // public abstract class ShieldSpell : SupportSpell {
    //     public override int Cost => 18;
    //     public override string Description => $"Add {ResistanceElement} to 1 ally. For 3 turns.";
    //     public override bool IsMultitarget => false;
    //     public abstract Elements ResistanceElement { get; }
    //     public virtual ResistanceModifiers ResistanceLevel { get; } = ResistanceModifiers.Resist;
    // }
}