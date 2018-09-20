using System.Collections;
using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Scenes
{
    public class CombatScene : MonoBehaviour
    {
        public Player P1;
        public Player P2;
        public Player Enemy;

        public Vector2 P1Location = new Vector2(0,0);
        public Vector2 P2Location = new Vector2(1, 0);
        public Vector2 EnemyLocation = new Vector2(2, 2);


        // Use this for initialization
        void Start()
        {
            StartCoroutine(Scenario1(0.2f));
        }

        private IEnumerator Scenario1(float time)
        {
            yield return new WaitForSeconds(time);

            var go = GameObject.Find($"Hex_{P1Location.x}_{P1Location.y}");
            var tile = go.GetComponent<Tile>();
            P1.Location = tile;
            tile.OccupiedBy = P1;
            P1.Location = tile;
            P1.transform.position = tile.transform.position + new Vector3(0, 0, -1);

            go = GameObject.Find($"Hex_{P2Location.x}_{P2Location.y}");
            tile = go.GetComponent<Tile>();
            P2.Location = tile;
            tile.OccupiedBy = P2;
            P2.Location = tile;
            P2.transform.position = tile.transform.position + new Vector3(0, 0, -1);

            go = GameObject.Find($"Hex_{EnemyLocation.x}_{EnemyLocation.y}");
            tile = go.GetComponent<Tile>();
            Enemy.Location = tile;
            tile.OccupiedBy = Enemy;
            Enemy.Location = tile;
            Enemy.transform.position = tile.transform.position + new Vector3(0, 0, -1);


        }
        
    }
}
