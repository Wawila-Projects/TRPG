using System;
using System.Collections;
using System.Collections.Generic;
using Assets.GameSystem;
using Assets.Utils;
using UnityEngine;

namespace Assets.EnemySystem
{
    public class EnemyDirector: MonoBehaviour
    {
        public List<Enemy> Enemies;
        public List<Hivemind> Hiveminds;

        public bool EnemiesActing;

        void Update()
        {
           Enemies = GameController.Manager?.Enemies ?? new List<Enemy>();

            if (!TurnManager.Manager.EnemyPhase)
                return;

            if (EnemiesActing)
                return;

            StartCoroutine(EnemyAct());
        }
        
        private IEnumerator EnemyAct()
        {
            EnemiesActing = true;
            Enemies.RemoveNull();
            foreach (var enemy in Enemies)
            {
                if (enemy.IsDead || enemy.TurnFinished)
                    continue;

                enemy.Act();
                yield return new WaitUntil(() => enemy.TurnFinished);
            }

            yield return new WaitUntil(() => Enemies.TrueForAll( e => e.TurnFinished ));
            TurnManager.Manager.NextTurn();
            EnemiesActing = false;
        }
    }
}