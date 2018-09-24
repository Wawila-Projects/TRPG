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

         void Awake()
         {
             Interactions = gameObject.GetComponent<PlayerInteractions>();
             SelectedText.color = Color.white;
             TargetText.color = Color.white;
             CombatText.color = Color.white;

             SelectedText.text = "";
             TargetText.text = "";
             CombatText.text = "";
         }

         void Update()
         {
             var selectedPlayer = Interactions.Selected;

             var target = Interactions.Target;
             var targetPlayer = target == null ? null : target.GetComponent<Player>();
            
             SetPlayerText(selectedPlayer, targetPlayer);
         }

         public void SetPlayerText(Player selectedPlayer, Player targetPlayer)
         {
             if (selectedPlayer == null)
             {
                 TargetText.text = "";
                 SelectedText.text = "";
                 return;
             }

             var selected =
                 $@"{selectedPlayer.name}: 
 Level: {selectedPlayer.Stats.Level}
 HP: {selectedPlayer.CurrentHealth}/{selectedPlayer.Stats.Hp}
 SP: {selectedPlayer.Stats.Sp} 
 Strength: {selectedPlayer.Stats.Strength}
 Magic: {selectedPlayer.Stats.Magic}
 Endurance: {selectedPlayer.Stats.Endurance}
 Agility: {selectedPlayer.Stats.Agility}
 Luck: {selectedPlayer.Stats.Luck}";

             SelectedText.text = selected;

             if (targetPlayer == null || selectedPlayer.IsEqualTo(targetPlayer))
             {
                 SelectedText.text = selected;
                 TargetText.text = "";
                 return;
             }

             var target =
                 $@"{targetPlayer.name}: 
Level: {targetPlayer.Stats.Level}
 HP: {targetPlayer.CurrentHealth}/{targetPlayer.Stats.Hp}
 SP: {targetPlayer.Stats.Sp} 
 Strength: {targetPlayer.Stats.Strength}
 Magic: {targetPlayer.Stats.Magic}
 Endurance: {targetPlayer.Stats.Endurance}
 Agility: {targetPlayer.Stats.Agility}
 Luck: {targetPlayer.Stats.Luck}";

            SelectedText.text = selected;
             TargetText.text = target;
         }
     }
 }