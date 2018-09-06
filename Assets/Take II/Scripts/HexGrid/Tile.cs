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
    }

    public static class TileUtils
    {
        public static bool IsEqualTo(this Tile t, Tile other)
        {
            var sameName = t.Name == other.Name;
            var sameCost = t.Cost == other.Cost;
            var sameTerrain = t.TerrainType == other.TerrainType;

            return sameName && sameCost && sameTerrain;
        }

        public static bool HasNeighbor(this Tile t, Tile other)
        {
            return t.Neighbors.Any(neighbor => neighbor.IsEqualTo(other));
        }

        public static Tile MoveAway(this Tile t, Tile other) { 
            var possibleTiles = other.Neighbors.Except(t.Neighbors);
            foreach (var tile in possibleTiles) {
                if (tile.OccupiedBy != null) 
                    continue;

                return tile;
            }
            return null;
        }

        private static Vector3 oddRToCube(this Tile t) {
            var x = t.GridY - (t.GridX - (t.GridX % 1)) / 2;
            var y = t.GridX;
            var z = -x-y;
            return new Vector3(x, y, z);
        }

        public static float Distance(this Tile t, Tile other) {
            var thisCube = t.oddRToCube();
            var otherCube = other.oddRToCube();

            var distance = Math.Max(Math.Abs(thisCube.x - otherCube.x),
                                    Math.Abs(thisCube.y - otherCube.y));
            distance = Math.Max(Math.Abs(thisCube.z - otherCube.z), distance);                  
            return distance;
        }
    }
}

