using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.PlayerManager;

namespace Assets.Take_II.Scripts.EnemyManager
{
    public class Enemy : Character
    {
        public int BasicAttack;

        void Awake()
        {
            Name = gameObject.name;
            Stats = new Stats();
            WeaponRange = IsRange ? 2 : 1;
            Stats.Hp = 100;
            CurrentHealth = Stats.Hp;
            Stats.Movement = 3;
            Stats.Endurance = 5;
            Stats.Strength = 10;
            BasicAttack = 100;
        }

        public void Act()
        {

        }
    }
}