using System.Collections;
using System.Collections.Generic;
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

        public List<GameObject> Neighbors;

        void Update()
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
                Neighbors.Add(go);
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
    }
}

