using System;
using System.Collections.Generic;
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
        private Player _target;

        public bool IsMoving { get; private set; }
        //private bool _isInteractingWithObject;
        public bool IsInCombat { get; private set; }
        public bool IsHealing { get; private set; }

        //private CombatManager combatManager;

        void Update()
        {
            if (IsMoving)
            {
                Debug.Log("Moving");
                Move();
                return;
            }

            if (_target != null)
            {
                OnPlayerAction();
            }
            else if (IsInCombat)
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

            if (player.IsEqualTo(Selected))
                return false;

            _target = player;

            if (!IsReachable(Selected.Location, player.Location))
            {
                MoveTowardsPlayer(player, false);
                clearMap = true;
                return true;
            }

            if (!player.Location.HasNeighbor(Selected.Location))
            {
                MoveTowardsPlayer(player, true);
                clearMap = true;
                return true;
            }

            clearMap = OnPlayerAction();
            return true;
        }

        private void MoveTowardsPlayer(Player player, bool isReachable)
        { 
            var pathfinding = new AStar();
            var path = pathfinding.FindPath(Selected.Location, player.Location);

            if (path.Count == 0) return;

            var length = isReachable ? path.Count - 2 : Selected.Stats.Movement;

            if (path.ElementAt(length) == null)
                return;

            
            while (path.ElementAt(length).GetComponent<Tile>().OccupiedBy != null)
            {
                if(path.ElementAt(length) == null)
                    return;
                
                length--;
            }
            

            Target = path.ElementAt(length < path.Count ? 0 : length).gameObject;
            IsMoving = true;
        }

        private bool OnPlayerAction()
        {
            
            if (_target == null || Selected == null)
                return false;

            if (_target.IsEnemy != Selected.IsEnemy)
                IsInCombat = true;
            else
                IsHealing = true;

            Target = _target.gameObject;
            _target = null;

            var clearMap = IsInCombat || IsHealing;
            return clearMap;
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
                Target = tile.OccupiedBy.gameObject;
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