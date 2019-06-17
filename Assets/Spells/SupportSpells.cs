using Assets.Enums;

namespace Assets.Spells {
    public abstract class SupportSpell : SpellBase {
        public override Elements Element => Elements.Recovery;
        public override bool IsMagical => true;
        public virtual int TurnsActive => 3;
        public virtual bool SingleUse => true;
        public abstract void Invoke();
    }

    public interface INegatableSpell {
        
    }
    public interface INonNegatableSpell {
        
    }
    public abstract class ShieldSpell : SupportSpell {
        public override int Cost => 18;
        public override string Description => $"Add {ResistanceElement} to 1 ally. For 3 turns.";
        public override bool IsMultitarget => false;
        public abstract Elements ResistanceElement { get; }
        public virtual ResistanceModifiers ResistanceLevel { get; } = ResistanceModifiers.Resist;
    }
    public interface INegationSpell {
        
    }
}