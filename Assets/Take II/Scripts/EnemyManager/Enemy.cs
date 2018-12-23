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

        public override void OnAwake()
        {
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
                if (player.IsDead)
                    continue;

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
            TurnFinished = true;
            IsSurrounded = false;
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

        public new Enemy ClonePlayer()
        {
            return GetComponent<Enemy>().gameObject.GetComponent<Enemy>();
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