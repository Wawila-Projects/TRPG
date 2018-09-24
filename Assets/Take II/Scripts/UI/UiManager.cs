using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.InputManger;
 using Assets.Take_II.Scripts.PlayerManager;
 using UnityEngine;
 using UnityEngine.UI;

 namespace Assets.Take_II.Scripts.UI
 {
     public class UiManager : MonoBehaviour
     {
         public Text TargetText;
         public Text SelectedText;
         public Text CombatText;
        
         public PlayerInteractions Interactions;
         public EnemyInteractions EnemyInteractions;

         void Awake()
         {
             Interactions = gameObject.GetComponent<PlayerInteractions>();
             EnemyInteractions = gameObject.GetComponent<EnemyInteractions>();
             SelectedText.color = Color.white;
             TargetText.color = Color.white;
             CombatText.color = Color.white;

             SelectedText.text = "";
             TargetText.text = "";
             CombatText.text = "";
         }

         void Update()
         {
             Character selectedPlayer = Interactions.Selected;

             if (selectedPlayer == null )
                 selectedPlayer = EnemyInteractions.Selected;

             var target = Interactions.Target;
             var targetPlayer = target == null ? null : target.GetComponent<Character>();
            
             SetPlayerText(selectedPlayer, targetPlayer);
         }

         public void SetPlayerText(Character selectedCharacter, Character targetCharacter)
         {
             if (selectedCharacter == null)
             {
                 TargetText.text = "";
                 SelectedText.text = "";
                 return;
             }

             var selected =
                 $@"{selectedCharacter.name}: 
 Level: {selectedCharacter.Stats.Level}
 HP: {selectedCharacter.CurrentHealth}/{selectedCharacter.Stats.Hp}
 SP: {selectedCharacter.Stats.Sp} 
 Strength: {selectedCharacter.Stats.Strength}
 Magic: {selectedCharacter.Stats.Magic}
 Endurance: {selectedCharacter.Stats.Endurance}
 Agility: {selectedCharacter.Stats.Agility}
 Luck: {selectedCharacter.Stats.Luck}";

             SelectedText.text = selected;

             if (targetCharacter == null || selectedCharacter.IsEqualTo(targetCharacter))
             {
                 SelectedText.text = selected;
                 TargetText.text = "";
                 return;
             }

             var target =
                 $@"{targetCharacter.name}: 
Level: {targetCharacter.Stats.Level}
 HP: {targetCharacter.CurrentHealth}/{targetCharacter.Stats.Hp}
 SP: {targetCharacter.Stats.Sp} 
 Strength: {targetCharacter.Stats.Strength}
 Magic: {targetCharacter.Stats.Magic}
 Endurance: {targetCharacter.Stats.Endurance}
 Agility: {targetCharacter.Stats.Agility}
 Luck: {targetCharacter.Stats.Luck}";

            SelectedText.text = selected;
             TargetText.text = target;
         }
     }
 }