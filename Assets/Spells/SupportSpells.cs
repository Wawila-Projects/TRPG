using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using Assets.Enums;
namespace Assets.Spells {
    public abstract class SupportSpell : CastableSpell {
        public sealed override bool IsMagical => true;
        protected sealed override string Id => $"SupportSpell_${Name}";
        public abstract IList<BuffEffect> Effects { get; }
        public sealed override Elements Element =>
            Effects[0].Buff ? Elements.Recovery : Elements.Ailment;
    }

    // public abstract class ShieldSpell : SupportSpell {
    //     public override int Cost => 18;
    //     public override string Description => $"Add {ResistanceElement} to 1 ally. For 3 turns.";
    //     public override bool IsMultitarget => false;
    //     public abstract Elements ResistanceElement { get; }
    //     public virtual ResistanceModifiers ResistanceLevel { get; } = ResistanceModifiers.Resist;
    // }
}