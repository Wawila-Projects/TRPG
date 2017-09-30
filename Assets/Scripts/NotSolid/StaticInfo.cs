using System.Collections.Generic;

namespace Assets.Scripts.NotSolid
{
    public static class StaticInfo{
        public static int CurrentMap = -1;
        public static List<Player> CurrentParty;
        public static Player SelectedPlayer = null;
        public static Player TargetPlayer = null;
        public static bool ClickedUnselectedUnit = false;
        public static bool IsTargeting = false; 
        public static bool MouseOverSelectedPlayer = false;
        public static bool MouseOverTargetPlayer = false;

        public static void Init()
        {
            CurrentParty = new List<Player> ();
        }
    }
}
