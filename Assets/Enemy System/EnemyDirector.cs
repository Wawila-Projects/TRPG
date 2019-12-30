using System;
using System.Collections;
using System.Collections.Generic;
using Assets.GameSystem;
using Assets.Utils;
using UnityEngine;

namespace Assets.EnemySystem {
    [RequireComponent (typeof (TurnManager))]
    [RequireComponent (typeof (GameController))]
    public class EnemyDirector : MonoBehaviour {
        public List<Enemy> Enemies;
        public List<Hivemind> Hiveminds;
        public TurnManager TurnManager;
        public GameController GameController;

        public bool EnemiesActing;

        void Awake () {
            Hiveminds = new List<Hivemind> {
                Hivemind.Global
            };
        }

        void Start () {
            Enemies = GameController.Enemies;
        }
        void Update () {
            if (Enemies.IsEmpty ()) {
                Enemies = GameController.Enemies;
            }

            if (!TurnManager.EnemyPhase || EnemiesActing || Enemies.IsEmpty ())
                return;

            StartCoroutine (EnemyAct ());
        }

        private IEnumerator EnemyAct () {
            EnemiesActing = true;
            foreach (var enemy in Enemies) {
                if (enemy is null) continue;
                
                if (enemy.IsDead || enemy.TurnFinished) {
                    enemy.TurnFinished = true;
                    continue;
                }

                enemy.Act ();
                yield return new WaitUntil (() => enemy.TurnFinished);
            }

            yield return new WaitUntil (() => Enemies.TrueForAll (e => e.TurnFinished));
            if (TurnManager.EnemyPhase) {
                TurnManager.NextTurn ();
                EnemiesActing = false;
            }
        }
    }
}