using System.Linq;
using Assets.Take_II.Scripts.Combat;
using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.EnemyManager
{
    public class Enemy : Character
    {
        public int BasicAttack;
        public Tile Destiny;
        public Player Target;
        private readonly AStar _pathfinding = new AStar();

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

        void Update()
        {
            if (Destiny != null)
            {
                Move();
                return;
            }

            if (Target == null) return;

            CombatManager.Manager.BasicAttack(this, Target);
            Target = null;
            TurnFinished = true;
        }

        public void Act()
        {
            Player target = null;
            foreach (var player in GameController.Manager.Players)
            {
                if (target == null)
                {
                    target = player;
                    continue;
                }

                var targetDistance = DistanceFromCombatRange(target);
                var playerDistance = DistanceFromCombatRange(player);

                if (playerDistance < targetDistance)
                    target = player;
            }
            ActOn(target);
        }

        public void ActOn(Tile tile)
        {
            var distance = Location.Distance(tile);
            if (!(distance > Movement)) return;
            var path = _pathfinding.FindPath(Location, tile);
            Destiny = path[Movement] ?? path.First();
        }

        public void ActOn(Player player)
        {
            Destiny = MoveToRange(player);
            Target = player;
        }

        private void Move()
        {
            var dest = new Vector3
            {
                x = Destiny.WorldX,
                y = Destiny.WorldY,
                z = transform.position.z
            };

            if (transform.position == dest)
            {
                Location.OccupiedBy = null;
                Destiny.OccupiedBy = this;
                Location = Destiny;
                Destiny = null;

                if (Target == null || DistanceFromCombatRange(Target) != 0)
                {
                    TurnFinished = true;
                }

                return;
            }

            var destination = Vector3.MoveTowards(transform.position, dest, 3 * Time.deltaTime);
            transform.position = destination;
        }
    }
}