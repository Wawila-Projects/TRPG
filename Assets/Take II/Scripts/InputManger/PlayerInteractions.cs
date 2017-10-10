using System;
using System.Linq;
using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class PlayerInteractions : MonoBehaviour
    {
        public Player Selected;
        public GameObject Target;

        public bool IsMoving { get; private set; }
        //private bool _isInteractingWithObject;
        public bool IsInCombat { get; private set; }
        public bool IsHealing { get; private set; }

        private readonly AStar _pathfinding = new AStar();

        //private CombatManager combatManager;

        void Update()
        {
            if (IsMoving)
            {
                Debug.Log("Moving");
                Move();
                return;
            }

            if (IsInCombat)
            {
                Debug.Log("Attacking: " + Target.name);
                IsInCombat = false;
                ClearSelected();
            }
            else if (IsHealing)
            {
                Debug.Log("Healing: " + Target.name);
                IsHealing = false;
                ClearSelected();
            }
        }

        public void Act(out bool clearMap)
        {
            clearMap = false;
            if (ActOnTile(ref clearMap)) return;

            ActOnPlayer(ref clearMap);

        }

        private bool ActOnPlayer(ref bool clearMap)
        {
            var player = Target.GetComponent<Player>();

            if (player == null) return false;

            if (!IsReachable(Selected.Location, player.Location))
            {
                var path = _pathfinding.FindPath(Selected.Location, player.Location);

                if(path.Count == 0 )
                    return false;

                Target = path.ElementAt(Selected.Stats.Movement).gameObject;
                IsMoving = true;
                clearMap = true;
                return true;
            }

            if (player.IsEnemy)
                IsInCombat = true;
            else
                IsHealing = true;

            clearMap = IsInCombat || IsHealing;
            return true;
        }

        private bool ActOnTile(ref bool clearMap)
        {
            var tile = Target.GetComponent<Tile>();
            if (tile == null) return false;

            if (!IsReachable(Selected.Location, tile))
            {
                ClearSelected();
                clearMap = true;
                return true;
            }

            if (tile.OccupiedBy != null)
            {
                Target = null;
                clearMap = false;
                return true;
            }

            IsMoving = true;
            clearMap = true;
            return true;
        }

        public void Move()
        { 
            var tile = Target.GetComponent<Tile>();
            if (tile == null)
                return;
           
            var dest = new Vector3
            {
                x = tile.WorldX,
                y = tile.WorldY,
                z = Selected.transform.position.z
            };

            if (Selected.transform.position == dest)
            {
                Selected.Location.OccupiedBy = null;
                tile.OccupiedBy = Selected;
                Selected.Location = tile;
                IsMoving = false;
                ClearSelected();
                return;
            }

            var destination = Vector3.MoveTowards(Selected.transform.position, dest, 3 * Time.deltaTime);
            Selected.transform.position = destination;
            
        }

        private bool IsReachable(Tile origin, Tile destiny)
        {
            var distance = Math.Max(Math.Abs(origin.GridX - destiny.GridX), Math.Abs(origin.GridY - destiny.GridY));
            return distance <= Selected.Stats.Movement;
        }

        private bool IsInMeleeRange()
        {
            var player = Target.GetComponent<Player>();
            if (player == null)
                return false;

            var distance = Math.Max(Math.Abs(Selected.Location.GridX - player.Location.GridX), 
                                    Math.Abs(Selected.Location.GridY - player.Location.GridY));
            
            return distance <= Selected.Stats.Movement+1;
        }

        private bool IsInRange()
        {
            var player = Target.GetComponent<Player>();
            if (player == null)
                return false;

            var distance = Math.Max(Math.Abs(Selected.Location.GridX - player.Location.GridX),
                Math.Abs(Selected.Location.GridY - player.Location.GridY));
            
            return distance <= Selected.Stats.Movement + 2;
        }

        private void ClearSelected()
        {
            Selected = null;
            Target = null;
        }
    }
}