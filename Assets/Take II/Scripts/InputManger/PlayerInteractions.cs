﻿using System;
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
        [SerializeField]
        private Player _target;
        [SerializeField]
        private Player _selected;

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

            _target = player;

            _selected = Selected.ClonePlayer();
            var path = _pathfinding.FindPath(Selected.Location, player.Location);

            if (path.Count == 0) return false;

            if (!IsReachable(Selected.Location, player.Location))
            {
                var mod = Selected.IsRange ? 1 : 0;
                Target = path.ElementAt(Selected.Stats.Movement - mod).gameObject;
                IsMoving = true;
                clearMap = true;
                return true;
            }

            if (!player.Location.HasNeighbor(Selected.Location) && !Selected.IsRange)
            {
                if (path.Count > 1)
                    path.RemoveAt(path.Count - 1);

                Target = path.Last().gameObject;
                IsMoving = true;
                clearMap = true;
                return true;
            }

            if (player.Location.HasNeighbor(Selected.Location) && Selected.IsRange) {
                var tile = player.Location.MoveAway(Selected.Location);
                if (tile == null) {
                    clearMap = false;
                    return false;
                }
                Target = tile.gameObject;
                IsMoving = true;
                clearMap = true;
                return true;
            }

            clearMap = OnPlayerAction();
            return true;
        }

        private bool OnPlayerAction()
        {
            if (_target == null || !_selected.IsInRange(_target))
                return false;
            
            if (_target.IsEnemy != _selected.IsEnemy)
                IsInCombat = true;
            else if(_selected.IsHealer)
                IsHealing = true;

            Target = _target.gameObject;
            Selected = _selected;
            _target = null;
            _selected = null;

            var clearMap = IsInCombat || IsHealing;
            return clearMap;
        }

        private bool ActOnTile(ref bool clearMap, bool moveTowardsUnreachable = false)
        {
            var tile = Target.GetComponent<Tile>();
            if (tile == null) return false;

            if (tile.OccupiedBy != null)
            {
                Target = tile.OccupiedBy.gameObject;
                clearMap = false;
                return true;
            }

            if (!IsReachable(Selected.Location, tile))
            {
                if (moveTowardsUnreachable)
                {
                    var path = _pathfinding.FindPath(Selected.Location, tile);
                    if (path.Count == 0) return false;
                    Target = path.ElementAt(Selected.Stats.Movement).gameObject;

                    IsMoving = true;
                }
                else
                {
                    ClearSelected();
                }

                clearMap = true;
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

        private void ClearSelected()
        {
            Selected = null;
            Target = null;
        }
    }
}