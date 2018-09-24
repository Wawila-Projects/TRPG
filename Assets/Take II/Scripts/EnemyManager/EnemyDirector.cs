using System.Collections.Generic;
using Assets.Take_II.Scripts.GameManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.EnemyManager
{
    public class EnemyDirector: MonoBehaviour
    {
        public List<Enemy> Enemies;

        void Update()
        {
           Enemies = GameController.Manager.Enemies;

            if (!TurnManager.Manager.EnemyPhase)
                return;

            foreach (var enemy in Enemies)
            {
                enemy.Act();
            }

            TurnManager.Manager.NextTurn();
        }
    }
}