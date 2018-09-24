using System.Collections;
using System.Collections.Generic;
using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Scenes
{
    public class CombatScene : MonoBehaviour
    {
        public List<Character> Characters = new List<Character>();
        public  List<Vector2> Positions = new List<Vector2>();

        void Start()
        {
            StartCoroutine(Scenario1(0.2f));
        }

        private IEnumerator Scenario1(float time)
        {
            yield return new WaitForSeconds(time);

            for (var i = 0; i < Characters.Count; i++)
            {
                var go = GameObject.Find($"Hex_{Positions[i].x}_{Positions[i].y}");
                var tile = go.GetComponent<Tile>();
                Characters[i].Location = tile;
                tile.OccupiedBy = Characters[i];
                Characters[i].Location = tile;
                Characters[i].transform.position = tile.transform.position + new Vector3(0, 0, -1);

            }
        }
        
    }
}
