// using System;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Assets.Take_II.Scripts.Combat
// {
//     public class GameManager: MonoBehaviour {
//         //public static GameManager Instance { get; } = new TurnManager();
//         //public List<Func<void, Character>> Actions = new List<Func<void, Character>>;
//         public List<Character> ActivePlayers = new List<Character>();
//         public List<Character> Enemies = new List<Character>();

//         private TurnManager _turnManager = new TurnManager();
//         private CombatManager _combatManager;
//         private uint _currentTurn = 0;

//         void Awake() {
//             _combatManager = gameObject.getComponent<CombatManager>()
//         }

//         void Update() {
//             if (_turnManager.TurnCounter == _currentTurn) {
//                 return;
//             }

//             foreach (var action : Actions) {
//                 action.Invoke()
//             }

//             _currnetTurn = _turnManager.NextTurn();
//         }

//         public void AddAction() {

//         }

//         public void EndOfTurn() {
            
//         }
//     }
// }