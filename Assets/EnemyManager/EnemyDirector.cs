using System;
using System.Collections;
using System.Collections.Generic;
using Assets.GameManager;
using UnityEngine;

namespace Assets.EnemyManager
{
    public class EnemyDirector: MonoBehaviour
    {
        public List<Enemy> Enemies;

        void Update()
        {
           Enemies = GameController.Manager?.Enemies ?? new List<Enemy>();

            if (!TurnManager.Manager.EnemyPhase)
                return;
            StartCoroutine(EnemyAct());
            TurnManager.Manager.NextTurn();
        }
        
        private IEnumerator EnemyAct()
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.IsDead)
                    continue;

                enemy.Act();
                yield return new WaitForSeconds(1);
                yield return new WaitUntil(() => enemy.TurnFinished);
            }
        }
    }
}