using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Player player;
	public Vector2 destination;

	public float speed;
	public Vector3 target;

	void Update() {
		
	}

	void LateUpdate () {
		if (StaticInfo.selectedPlayer != null && StaticInfo.selectedPlayer.CompareTag("Player")) {
			player = (Player)StaticInfo.selectedPlayer;
			setTargetMousePosition ();
			move ();
			if (isCancelable ()) {
				StaticInfo.selectedPlayer = null;
			}
		
		}    
	}
		
	bool isCancelable() {
		bool inDestination = false, buttonDown = false;

		if (target == player.transform.position)
			inDestination = true;

		if (Input.GetKeyDown (KeyCode.Escape))
			buttonDown = true;

		return inDestination && buttonDown;
	}

	void setTarget(Vector3 target) {
		this.target = target;
	}

	void setTargetMousePosition ()
	{
		if (Input.GetMouseButtonDown (0) && !StaticInfo.clickedUnselectedUnit && !StaticInfo.isTargeting) {
			target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			target.z = player.transform.position.z;
			target.x = Mathf.RoundToInt (target.x);
			target.y = Mathf.RoundToInt (target.y);
		} 
	}

	void move()
	{
		destination = Vector3.MoveTowards (player.transform.position, target, speed * Time.deltaTime);			
		player.transform.position = destination;
	}
}
