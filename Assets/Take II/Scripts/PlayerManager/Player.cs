using Assets.Take_II.Scripts.GameManager;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class Player: Character
    {

        public Equipment Equipment;
        
        public override void OnAwake() {
            Equipment = new Equipment
            {
                Armor = 10,
                AttackPower = 100
            };
        }
        
        public new Player ClonePlayer()
        {
            var temp = Instantiate(this);
            var player = temp.GetComponent<Player>();
            Destroy(temp.gameObject);
            return player;
        }
    }
}