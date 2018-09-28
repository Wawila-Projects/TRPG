using System;
using Assets.Take_II.Scripts.GameManager;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class Player: Character
    {

        public Equipment Equipment;

        public override void onAwake() {
            Equipment = new Equipment();
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