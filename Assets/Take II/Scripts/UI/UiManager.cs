using System.Collections.Generic;
using Assets.Take_II.Scripts.Combat;
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
        
        private PlayerInteractions _interactions;

        void Awake()
        {
            _interactions = gameObject.GetComponent<PlayerInteractions>();
            SelectedText.color = Color.white;
            TargetText.color = Color.white;
            CombatText.color = Color.white;

            SelectedText.text = "";
            TargetText.text = "";
            CombatText.text = "";
        }

        void Update()
        {
            var selectedPlayer = _interactions.Selected;

            var target = _interactions.Target;
            var targetPlayer = target == null ? null : target.GetComponent<Player>();
            
            SetPlayerText(selectedPlayer, targetPlayer);


            if(targetPlayer != null)
                SetCombatText(targetPlayer, selectedPlayer.IsHealer && selectedPlayer.IsEnemy == targetPlayer.IsEnemy);
            else
                CombatText.text = "";
        }


        public void SetCombatText(Player targetPlayer, bool isHealing = false)
        {
            var combatManager = gameObject.GetComponent<CombatManager>();

            if (combatManager == null || combatManager.Defender == null)
            {
                CombatText.text = "";
                return;
            }

            var combatInfo = combatManager.GetCombatStats();

            var text = "";

            if (isHealing)
            {
                text = $@"Healing: 
HP: {combatInfo["hp"]} -> {combatInfo["newhp"]}";
            }
            else if(_interactions.Selected.IsEnemy != targetPlayer.IsEnemy)
            {
                text = $@"Combat: 
HP: {combatInfo["hp"]} -> {combatInfo["newhp"]}
Hit: {combatInfo["hit"]}
Attack: {combatInfo["att"]}
Crit: {combatInfo["crit"]}";
            }

            CombatText.text = text;
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
Hp: {selectedPlayer.CurrentHealth}/{selectedPlayer.Stats.Hp}
Str: {selectedPlayer.Stats.Str}
Mag: {selectedPlayer.Stats.Mag}
Skl: {selectedPlayer.Stats.Skl}
Spd: {selectedPlayer.Stats.Spd}
Def: {selectedPlayer.Stats.Def}
Res: {selectedPlayer.Stats.Res}
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
Hp: {targetPlayer.CurrentHealth}/{targetPlayer.Stats.Hp}
Str: {targetPlayer.Stats.Str}
Mag: {targetPlayer.Stats.Mag}
Skl: {targetPlayer.Stats.Skl}
Spd: {targetPlayer.Stats.Spd}
Def: {targetPlayer.Stats.Def}
Res: {targetPlayer.Stats.Res}
Luck: {targetPlayer.Stats.Luck}";

            SelectedText.text = selected;
            TargetText.text = target;

        }
    }
}