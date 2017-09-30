using System.Collections.Generic;
using UnityEngine;

namespace Assets.Take_II.Scripts.HexGrid
{
    public class HexManager : MonoBehaviour
    {

        public GameObject Prefab;
        public int[,] Map;

        public int XSize = 19;
        public int YSize = 12;

        private const float XOffset = 0.882f;
        private const float YOffset = 0.764f;


        void Awake()
        {
            var rand = new System.Random();

            for (var x = 0; x < XSize; x++)
            {
                for (var y = 0; y < YSize; y++)
                {
                    var xPos = x * XOffset;
                    var yPos = y * YOffset;

                    if (y % 2 == 1)
                    {
                        xPos += XOffset / 2;
                    }

                    var tile = Instantiate(Prefab, new Vector3(xPos, yPos), Quaternion.identity);
                    tile.name = "Hex_" + x + "_" + y;
                    tile.transform.SetParent(transform);

                    var t = tile.GetComponent<Tile>();
                    t.Name = tile.name;
                    t.WorldX = xPos;
                    t.WorldY = yPos;
                    t.GridX = x;
                    t.GridY = y;
                    t.Cost = rand.Next(0, 10);
                }
            }
        }
    }

}

