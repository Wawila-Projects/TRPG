using Assets.Scripts.Interfaces.Characters;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter {

	void Start()
	{
		if(CompareTag("Player"))
			StaticInfo.currentParty.Add (this);
	}
		
	void OnMouseOver()
	{
		if (this == StaticInfo.targetPlayer)
			StaticInfo.mouseOverTargetPlayer = true;

		if(canTargetPlayer()) {
			if (StaticInfo.isTargeting) 
				StaticInfo.targetPlayer = this;
			
			if (this != StaticInfo.selectedPlayer)
				StaticInfo.clickedUnselectedUnit = true;
		}

		if (this.CompareTag ("Player"))
			playerAction ();
	}

	void playerAction()
	{
		if (this == StaticInfo.selectedPlayer)
			StaticInfo.mouseOverSelectedPlayer = true;

		if (StaticInfo.selectedPlayer == null && Input.GetMouseButton (0)) {
			StaticInfo.selectedPlayer = this;
			StaticInfo.targetPlayer = null;
		}

	}

	bool canTargetPlayer()
	{
		bool selectedPlayerExist = false, correctInput = false;

		if (StaticInfo.selectedPlayer != null)
			selectedPlayerExist = true;

		if (Input.GetMouseButton (0))
			correctInput = true;

			return selectedPlayerExist && correctInput;
	}

	void OnMouseExit()
	{
		StaticInfo.clickedUnselectedUnit = false;
		StaticInfo.mouseOverTargetPlayer = false;
		StaticInfo.mouseOverSelectedPlayer = false;
	}
		
}
