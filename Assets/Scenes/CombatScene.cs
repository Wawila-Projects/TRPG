using System.Collections;
using System.Collections.Generic;
using Assets.Take_II.Scripts.GameManager;
using UnityEngine;

namespace Assets.Scenes {
    public class CombatScene : MonoBehaviour {

        public MapCoordinator Map;

        public List<Character> Characters = new List<Character> ();
        public List<Vector3> Positions = new List<Vector3> ();

        void Start () {
            StartCoroutine (Scenario1 ());
        }

        private IEnumerator Scenario1 () {
            yield return new WaitUntil (() => Map.DoneShowing);

            for (var i = 0; i < Characters.Count; i++) {
                var go = Map.Map.Find ((T) => T.Hex == Positions[i]);
                var tile = go.GetComponent<Tile> ();
                var character = Characters[i];
                character.Location = tile;
                tile.Occupant = character;
                character.Location = tile;
                character.transform.position = tile.transform.position;

                var levelDifference = 60 - character.Level;
                for(var j = 0; j < levelDifference; j++) {
                    character.LevelUp();
                }
            }
        }
    }
}