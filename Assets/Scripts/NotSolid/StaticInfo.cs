using System.Collections.Generic;

public static class StaticInfo{
	public static int currentMap = -1;
	public static List<Player> currentParty;
	public static Player selectedPlayer = null;
	public static Player targetPlayer = null;
	public static bool clickedUnselectedUnit = false;
	public static bool isTargeting = false; 
	public static bool mouseOverSelectedPlayer = false;
	public static bool mouseOverTargetPlayer = false;

	public static void init()
	{
		currentParty = new List<Player> ();
	}
}
