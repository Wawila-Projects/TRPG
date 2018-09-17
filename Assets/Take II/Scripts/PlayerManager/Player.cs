using System;
using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;
using System.Linq;
namespace Assets.Take_II.Scripts.PlayerManager
{
    public class Player : MonoBehaviour
    {
        public string Name;
        public Tile Location;
        public int CurrentHealth;
        public int CurrentActionPoints;

        //public Equipment Equipment;
        
        public Stats Stats;
        public ActionHandler ActionsHandler;

        public int Movement => Stats.Movement;
        public bool IsDead;
        public bool IsEnemy;
        public bool IsHealer;
        public bool IsRange;
        public int WeaponRange;

        void Awake()
        {
            Name = gameObject.name;   
            WeaponRange = IsRange ? 2 : 1;
            CurrentHealth = Stats.Hp;
            Stats.Movement = 3;
        }

        void Update()
        {
            if(CurrentHealth > 0)
                return;

            CurrentHealth = 0;
            IsDead = true;
        }

        public void EndTurn() {
            CurrentActionPoints = ActionsHandler.ActionPoints;
        }

         public bool IsEqualTo(Player other)
        {
            var leftNull = this == null;
            var rightNull = other == null;

            if (leftNull || rightNull)
                return false;

            var sameName = name == other.name;
            var sameLocation = Location.IsEqualTo(other.Location);

            return sameName && sameLocation;
        }

        public Player ClonePlayer()
        {
            var temp = Object.Instantiate(this);
            var player = temp.GetComponent<Player>();
            Object.Destroy(temp.gameObject);
            return player;
        }

        public bool IsInRange(Player other)
        {
            if (other == null)
                return false;

            var distance = Location.Distance(other.Location);

            return distance <= Stats.Movement + WeaponRange;
        }

        public int DistanceFromCombatRange(Player other)
        {
            var distance = Location.Distance(other.Location);
            return (int)(distance - WeaponRange);
        }

         public Tile MoveAway(Player other) 
         { 
            var possibleTiles = Location.Neighbors.Except(other.Location.Neighbors);
            foreach (var tile in possibleTiles) 
            {
                if (tile.OccupiedBy == null) 
                    return tile;
            }
            return null;
        }

        public Tile MoveTowards(Player other, int steps) 
        {
            var totalSteps = steps > Movement ? Movement : steps;
            var pathfinder = new AStar();
            var path = pathfinder.FindPath(Location, other.Location);
            path.Remove(Location);
            if (path.Count > steps) 
            {
                while (path.Count != totalSteps) 
                    path.Remove(path.Last());
            }
            var tile = path.Last();
            if (tile.OccupiedBy == null)
                return tile;
        
            return MoveTowardsIfOccupied(tile, other, pathfinder);
        }

        private Tile MoveTowardsIfOccupied(Tile tile, Player other, AStar pathfinder) 
        {
            foreach (var neighbor in tile.Neighbors) 
            {
                var optPath = pathfinder.FindPath(Location, neighbor);
                optPath.Remove(Location);
                
                var isReachable = optPath.Count <= Movement; 
                var isInRange = neighbor.DistanceFromCombatRange(this, other) == 0;
                var isNotOccupied = neighbor.OccupiedBy == null;

                if (isReachable && isInRange && isNotOccupied)
                    return neighbor;
            }
            return null;
        }

        public Tile MoveToRange(Player other) {
            var distance = DistanceFromCombatRange(other);
            if (distance > 0 )
                return MoveTowards(other, (int)distance);
            if (distance < 0) 
                return MoveAway(other);
            return null;
        }
    }
}