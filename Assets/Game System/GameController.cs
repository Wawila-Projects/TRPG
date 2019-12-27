using System.Collections.Generic;
using Assets.EnemySystem;
using Assets.PlayerSystem;
using Assets.Scenes;
using Assets.UI;
using UnityEngine;

namespace Assets.GameSystem {

    [RequireComponent (typeof (CombatScene))]
    public class GameController : MonoBehaviour {
        public static GameController Manager { get; private set; }

        public UiManager UIManager;
        public CombatScene Scene;

        public List<Player> Players { get; private set; } = new List<Player> ();

        public List<Enemy> Enemies { get; private set; } = new List<Enemy> ();

        void Awake () {
            Manager = this;

            foreach(var character in Scene.Characters) {
                if (character is Player player) {
                    Players.Add(player);
                }
                
                if (character is Enemy enemy) {
                    Enemies.Add(enemy);
                }
            }
        }

        void Start () {
            ///TODO: Temporary Fix
            foreach (var enemy in Enemies) {
                enemy.AI.Hivemind.ResetHivemind ();
            }

        }
    }
}