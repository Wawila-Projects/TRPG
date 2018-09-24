using System;
using Assets.Take_II.Scripts.GameManager;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class Player: Character
    {

        public Equipment Equipment;

        void Awake()
        {
            Name = gameObject.name;
            Stats = new Stats();
            Equipment = new Equipment();
            WeaponRange = IsRange ? 2 : 1;
            Stats.Hp = 100;
            CurrentHealth = Stats.Hp;
            Stats.Movement = 3;
            Stats.Endurance = 5;
            Stats.Strength = 10;
            Equipment.Armor = 10;
            Equipment.AttackPower = 100;
        }
        public new Player ClonePlayer()
        {
            var temp = Instantiate(this);
            var player = temp.GetComponent<Player>();
            Destroy(temp.gameObject);
            return player;
        }

        public static explicit operator Player(int v)
        {
            throw new NotImplementedException();
        }
    }
}