using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

	public int index;
	public Movement movement;

	void Awake()
	{
		StaticInfo.init ();
	}

	void Update ()
	{
		if (Input.GetMouseButtonUp (1))
			StaticInfo.targetPlayer = null;

		if (Input.GetKey (KeyCode.Space)) {
			StaticInfo.isTargeting = true;
		} else
			StaticInfo.isTargeting = false;

		iterateThroughPlayerList ();
	}


	void iterateThroughPlayerList()
	{
		
		 index = StaticInfo.currentParty.IndexOf (StaticInfo.selectedPlayer);

		if (index == -1)
			index = 0;

		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (Input.GetKey (KeyCode.RightShift) || Input.GetKey (KeyCode.LeftShift))
				index--;
			else
				index++;
			
			if (index < 0)
				index = StaticInfo.currentParty.Count-1;
			if (index > StaticInfo.currentParty.Count-1)
				index = 0;


			StaticInfo.selectedPlayer = StaticInfo.currentParty [index];
			movement.target = StaticInfo.selectedPlayer.transform.position;
		}

	}
}

