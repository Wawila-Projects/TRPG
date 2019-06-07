using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Assets.GameSystem;
using Assets.CharacterSystem;
using UnityEngine;

namespace Assets.PlayerSystem
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