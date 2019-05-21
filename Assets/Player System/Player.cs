using Assets.GameSystem;

namespace Assets.PlayerSystem
{
    public class Player: Character
    {
        public Equipment Equipment;
        
        protected override void OnAwake() {
            Equipment = new Equipment
            {
                Armor = 10,
                AttackPower = 100,
                Accuracy = 10
            };
        }
        
        public new Player ClonePlayer()
        {
            return GetComponent<Player>().gameObject.GetComponent<Player>();
        }
    }
}