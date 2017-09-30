using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.NotSolid
{
    public class StaticInfoPrinter : MonoBehaviour
    {
        public int CurrentMap;
        public List<Player> CurrentParty;
        public Player SelectedPlayer;
        public Player TargetPlayer;
        public bool ClickedUnselectedUnit;
        public bool IsTargeting;
        public bool MouseOverSelectedPlayer;
        public bool MouseOverTarget;

        void Update()
        {
            CurrentMap = StaticInfo.CurrentMap;
            CurrentParty = StaticInfo.CurrentParty;
            SelectedPlayer = StaticInfo.SelectedPlayer;
            TargetPlayer = StaticInfo.TargetPlayer;
            ClickedUnselectedUnit = StaticInfo.ClickedUnselectedUnit;
            IsTargeting = StaticInfo.IsTargeting;
            MouseOverSelectedPlayer = StaticInfo.MouseOverSelectedPlayer;
            MouseOverTarget = StaticInfo.MouseOverTargetPlayer;
        }

    }
}

