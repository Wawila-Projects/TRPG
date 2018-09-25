using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Assets.Take_II.Scripts.GameManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class PlayerDirector: MonoBehaviour
    {
        public List<Player> Players;

        void Update()
        {
           Players = GameController.Manager.Players;

            if (!TurnManager.Manager.PlayerPhase)
                return;

            var finishedPlayers = Players.All(p => p.TurnFinished);
            
            if (finishedPlayers)
                TurnManager.Manager.NextTurn();
        }
    }
}