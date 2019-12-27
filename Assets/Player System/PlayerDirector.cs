using System.Collections.Generic;
using System.Linq;
using Assets.GameSystem;
using Assets.Utils;
using UnityEngine;

namespace Assets.PlayerSystem {
    [RequireComponent (typeof (TurnManager))]
    [RequireComponent (typeof (GameController))]
    public class PlayerDirector : MonoBehaviour {
        public List<Player> Players;
        public TurnManager TurnManager;
        public GameController GameController;

        void Start () {
            Players = GameController.Players;
        }

        void Update () {
            if (Players.IsEmpty ()) {
                Players = GameController.Manager.Players;
            }

            if (!TurnManager.PlayerPhase || Players.IsEmpty ())
                return;

            var finishedPlayers = Players.All (p => p.TurnFinished);

            if (finishedPlayers && TurnManager.PlayerPhase)
                TurnManager.NextTurn ();
        }
    }
}