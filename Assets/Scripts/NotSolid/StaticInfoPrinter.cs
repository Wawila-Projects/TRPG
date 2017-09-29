using UnityEngine;
using System.Collections.Generic;

public class StaticInfoPrinter : MonoBehaviour
{
	public int currentMap;
	public List<Player> currentParty;
	public Player selectedPlayer;
	public Player targetPlayer;
	public bool clickedUnselectedUnit;
	public bool isTargeting;
	public bool mouseOverSelectedPlayer;
	public bool mouseOverTarget;

	void Update()
	{
	 	currentMap = StaticInfo.currentMap;
		currentParty = StaticInfo.currentParty;
		selectedPlayer = StaticInfo.selectedPlayer;
		targetPlayer = StaticInfo.targetPlayer;
	    clickedUnselectedUnit = StaticInfo.clickedUnselectedUnit;
		isTargeting = StaticInfo.isTargeting;
		mouseOverSelectedPlayer = StaticInfo.mouseOverSelectedPlayer;
		mouseOverTarget = StaticInfo.mouseOverTargetPlayer;
	}

}

