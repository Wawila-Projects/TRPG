using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Assets.GameManager;
using UnityEngine;

namespace Assets.PlayerManager
{
    public class PlayerDirector: MonoBehaviour
    {
        public List<Player> Players;

        void Update()
        {
           Players = GameController.Manager?.Players ?? new List<Player>();

            if (!TurnManager.Manager.PlayerPhase)
                return;

            var finishedPlayers = Players.All(p => p.TurnFinished);
            
            if (finishedPlayers)
                TurnManager.Manager.NextTurn();
        }
    }
}