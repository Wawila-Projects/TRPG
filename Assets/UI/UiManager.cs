// using Assets.GameManager;
// using Assets.InputManger;
// using UnityEngine;
//  using UnityEngine.UI;

//  namespace Assets.UI
//  {
//      public class UiManager : MonoBehaviour
//      {
//          public Text TargetText;
//          public Text SelectedText;
//          public Text CombatText;
//          public Button EndTurnButton;

//          public PlayerInteractions Interactions;
//          public EnemyInteractions EnemyInteractions;

//          void Awake()
//          {
//              SelectedText.color = Color.white;
//              TargetText.color = Color.white;
//              CombatText.color = Color.white;

//              SelectedText.text = "";
//              TargetText.text = "";
//              CombatText.text = "";
//          }

//          void Update()
//          {

//             var turnManager = TurnManager.Manager;
//             EndTurnButton.enabled = turnManager.PlayerPhase || turnManager.Preround;
//              var turnName = turnManager.Preround ? "Preround:" :
//                  turnManager.PlayerPhase ? "Player Phase:" : "Enemy Phase"; 
//             CombatText.text = $"{turnName}\nTurn: {TurnManager.Manager.TurnCounter}";
             
//             Character selectedPlayer = Interactions.Selected;

//             if (selectedPlayer == null )
//                 selectedPlayer = EnemyInteractions.Selected;

//             var target = Interactions.Target;
//             var targetPlayer = target == null ? null : target.GetComponent<Character>();
            
//             SetPlayerText(selectedPlayer, targetPlayer);
//          }

//          public void SetPlayerText(Character selectedCharacter, Character targetCharacter)
//          {
//              if (selectedCharacter == null)
//              {
//                  TargetText.text = "";
//                  SelectedText.text = "";
//                  return;
//              }

//              var selected =
//                  $@"{selectedCharacter.name}: 
//  Level: {selectedCharacter.Stats.Level}
//  HP: {selectedCharacter.CurrentHealth}/{selectedCharacter.Stats.Hp}
//  SP: {selectedCharacter.Stats.Sp} 
//  Strength: {selectedCharacter.Stats.Strength}
//  Magic: {selectedCharacter.Stats.Magic}
//  Endurance: {selectedCharacter.Stats.Endurance}
//  Agility: {selectedCharacter.Stats.Agility}
//  Luck: {selectedCharacter.Stats.Luck}";

//              SelectedText.text = selected;

//              if (targetCharacter == null || selectedCharacter.IsEqualTo(targetCharacter))
//              {
//                  SelectedText.text = selected;
//                  TargetText.text = "";
//                  return;
//              }

//              var target =
//                  $@"{targetCharacter.name}: 
// Level: {targetCharacter.Level}
//  HP: {targetCharacter.CurrentHealth}/{targetCharacter.Hp}
//  SP: {targetCharacter.Sp} 
//  Strength: {targetCharacter.Stats.Strength}
//  Magic: {targetCharacter.Stats.Magic}
//  Endurance: {targetCharacter.Stats.Endurance}
//  Agility: {targetCharacter.Stats.Agility}
//  Luck: {targetCharacter.Stats.Luck}";

//             SelectedText.text = selected;
//              TargetText.text = target;
//          }

//          public void EndTurnButtonPressed()
//          {
//              TurnManager.Manager.NextTurn();
//          }

//      }
//  }