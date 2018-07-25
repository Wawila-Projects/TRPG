using System.Collections.Generic;
using System.Linq;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;


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

        public void AddNeighbor(int x, int y)
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

        public static Vector2 GetDirection(this Tile origin, Tile destiny) {
            var Oz = -origin.GridX - origin.GridY;
            var Dz = -destiny.GridX - destiny.GridY;

            if (Oz == Dz) 
                return new Vector2(destiny.GridX - origin.GridX, destiny.GridY - origin.GridY);
            

            var X = Mathf.Abs(destiny.GridX - origin.GridX) * (destiny.GridX - origin.GridX);
            var Y = Mathf.Abs(destiny.GridY - origin.GridY) * (destiny.GridY - origin.GridY);
    
            return new Vector2(X, Y);
        }

        public static Vector2 GetOpposite(this Tile t, Tile other){ 
            var difference = t.GetDirection(other);
            return difference * 2
        }
    }
}

