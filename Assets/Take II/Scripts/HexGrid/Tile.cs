using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System;
using Assets.Take_II.Scripts.GameManager;

namespace Assets.Take_II.Scripts.HexGrid
{
    //? Is MonoBehaviout necessary?
    public class Tile : MonoBehaviour
    {
        public string Name;
        public float WorldX;
        public float WorldY;
        public int GridX;
        public int GridY;
        public int TerrainType;
        public int Cost;
        public Character OccupiedBy;

        public List<Tile> Neighbors;

        void LateUpdate()
        {
            if(Neighbors.Count == 0)
                GetNeighbors();
        }

        public void GetNeighbors()
        {
            AddNeighbor(-1, 0);
            AddNeighbor(0, -1);
            AddNeighbor(1, 0);
            AddNeighbor(0, 1);


            if (GridY % 2 == 0)
            {
                AddNeighbor(-1, -1);
                AddNeighbor(-1, 1);
            }
            else
            {
                AddNeighbor(1, -1);
                AddNeighbor(1, 1);
            }
        }

        public List<Tile> GetTilesAtDistance(int distance) {
            if(distance == 1) {
                return Neighbors;
            } 
            
            var tiles = new List<Tile>();
            
            

            return tiles;
        }

        private void AddNeighbor(int x, int y)
        {
            var go = GameObject.Find("Hex_" + (GridX + x) + "_" + (GridY + y));
            if (go != null)
                Neighbors.Add(go.GetComponent<Tile>());
        }
    
        public bool IsEqualTo(Tile other)
        {
            var sameName = Name == other.Name;
            var sameCost = Cost == other.Cost;
            var sameTerrain = TerrainType == other.TerrainType;

            return sameName && sameCost && sameTerrain;
        }

        public bool HasNeighbor(Tile other)
        {
            return Neighbors.Any(neighbor => neighbor.IsEqualTo(other));
        }

        private Vector3 OddRToCube() 
        {
            var x = GridX - (GridY - (GridY % 1)) / 2;
            var y = GridY;
            var z = -x-y;
            return new Vector3(x, y, z);
        }

        public float Distance(Tile other) 
        {
            var thisCube = OddRToCube();
            var otherCube = other.OddRToCube();

            var distance = Math.Max(Math.Abs(thisCube.x - otherCube.x),
                                    Math.Abs(thisCube.y - otherCube.y));
            distance = Math.Max(Math.Abs(thisCube.z - otherCube.z), distance);                  
            return distance;
        }

        public int DistanceFromCombatRange(Character character, Character other) 
        {
            var distance = Distance(other.Location);
            return (int)(distance - character.WeaponRange);
        }

        public bool IsReachable(Tile destiny, int amount)
        {
            var distance = Distance(destiny);
            return distance <= amount;
        }

        public void OnDrawGizmos() {
            var text = $"({GridX}, {GridY})";
            var style = new GUIStyle() {
                fontSize = 4
            };
            var position = transform.position;
            position.x -= 0.2f; 
            Handles.Label(position, text, style);
        }
    }
}

