using System.Collections.Generic;
using System.Linq;
using Assets.EnemySystem;
using Assets.PlayerSystem;
using Assets.CharacterSystem;
using UnityEngine;
using Assets.UI;

namespace Assets.GameSystem
{
    public class GameController : MonoBehaviour
    {
        public static GameController Manager {get; private set;}

        public UiManager UIManager;

        public List<Player> Players
        {
            get { return _players; }
            set { _players = new List<Player>(value.OrderByDescending(p => p.Persona.Agility)); }
        }

        public List<Enemy> Enemies
        {
            get { return _enemies; }
            set { _enemies = new List<Enemy>(value.OrderByDescending(p => p.Persona.Agility)); }
        }

        [SerializeField]
        private List<Player> _players = new List<Player>();

        [SerializeField]
        private List<Enemy> _enemies = new List<Enemy>();

        void Awake()
        {
            Manager = this;
        }

        void Start () 
        {
            ///TODO: Temporary Fix
            foreach (var enemy in Enemies)
            {
                enemy.AI.Hivemind.ResetHivemind();
            }

        }
    }
}