namespace Assets.Scripts.Interfaces.Attacks
{
    public interface IAttackCastable {
        void AttackCast ();
    }

    public interface IAttackInterruptable {
        void AttackInterrupt ();
    }
	
    public interface IAttackBlockable {
        void AttackBlock ();
    }

    public interface IFullAttack : IAttackCastable, IAttackBlockable, IAttackInterruptable { }
    
    public interface IMagic<T> {
    }

    public interface IMelee {
    }

    public interface IRange {
    }
}