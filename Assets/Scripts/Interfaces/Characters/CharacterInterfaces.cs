using Assets.Scripts.Interfaces.Attacks;

namespace Assets.Scripts.Interfaces.Characters
{
    public interface IDamageable<T> {
        void Damage(T damageAmount);
    }

    public interface IKillabel {
        void Die ();
    }

    public interface IHealable<T> {
        void Heal (T healAmount);
    }

    public interface IRevivable {
        void Revive ();
    }
	
    public interface IMoveable<T> {
        void Move (T moveAmount);
    }
    
    public interface IFullCharacter<H, M, D> : IKillabel, IHealable<H>, IRevivable, IMoveable<M>, IDamageable<D>{}

    public interface ISpellCaster<T> {
        void CastSpell (IMagic<T> spell);
    }

    public interface ISwordSwinger {
        void SwingSword (IMelee melee);
    }

    public interface IGunSlinger {
        void SlingGun (IRange range);
    }	

    public interface IFullFighter<T> : ISpellCaster<T>, ISwordSwinger, IGunSlinger {}
}