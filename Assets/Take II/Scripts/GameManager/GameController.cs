using System.Collections.Generic;
using System.Linq;
using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.GameManager
{
    public class GameController : MonoBehaviour
    {
        public static GameController Manager { get; private set; }

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
    }
}