using System.Collections.Generic;
using System.Linq;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;
using System;

namespace Assets.Take_II.Scripts.HexGrid
{
    public class Tile : MonoBehaviour
    {
        public string Name;
        public float WorldX;
        public float WorldY;
        public int GridX;
        public int GridY;
        public int TerrainType;
        public int Cost;
        public Player OccupiedBy;

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

        private Vector3 oddRToCube() {
            var x = GridX - (GridY - (GridY % 1)) / 2;
            var y = GridY;
            var z = -x-y;
            return new Vector3(x, y, z);
        }

        public float Distance(Tile other) {
            var thisCube = oddRToCube();
            var otherCube = other.oddRToCube();

            var distance = Math.Max(Math.Abs(thisCube.x - otherCube.x),
                                    Math.Abs(thisCube.y - otherCube.y));
            distance = Math.Max(Math.Abs(thisCube.z - otherCube.z), distance);                  
            return distance;
        }
    }
}

