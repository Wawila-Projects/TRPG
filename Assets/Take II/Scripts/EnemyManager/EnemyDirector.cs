using System.Collections.Generic;
using Assets.Take_II.Scripts.Combat;
using UnityEngine;

namespace Assets.Take_II.Scripts.EnemyManager
{
    public class EnemyDirector: MonoBehaviour
    {
        public List<Enemy> Enemies;

        void Awake()
        {
            Enemies = new List<Enemy>();
        }

        void Update()
        {
            if (TurnManager.Manager.PlayerPhase)
                return;

            foreach (var enemy in Enemies)
            {
                enemy.Act();
            }

            TurnManager.Manager.NextTurn();
        }
    }
}