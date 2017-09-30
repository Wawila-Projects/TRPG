using UnityEngine;

namespace Assets.Scripts.NotSolid
{
    public class Movement : MonoBehaviour {

        public Player Player;
        public Vector2 Destination;

        public float Speed;
        public Vector3 Target;

        void LateUpdate () {
            if (StaticInfo.SelectedPlayer != null && StaticInfo.SelectedPlayer.CompareTag("Player")) {
                Player = StaticInfo.SelectedPlayer;
                SetTargetMousePosition ();
                Move ();
                if (IsCancelable ()) {
                    StaticInfo.SelectedPlayer = null;
                }
		
            }    
        }
		
        bool IsCancelable() {
            bool inDestination = false, buttonDown = false;

            if (Target == Player.transform.position)
                inDestination = true;

            if (Input.GetKeyDown (KeyCode.Escape))
                buttonDown = true;

            return inDestination && buttonDown;
        }

        private void SetTarget(Vector3 target) {
            this.Target = target;
        }

        private void SetTargetMousePosition ()
        {
            if (!Input.GetMouseButtonDown(0) || StaticInfo.ClickedUnselectedUnit || StaticInfo.IsTargeting) return;

            Target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Target.z = Player.transform.position.z;
            Target.x = Mathf.RoundToInt (Target.x);
            Target.y = Mathf.RoundToInt (Target.y);
        }

        void Move()
        {
            Destination = Vector3.MoveTowards (Player.transform.position, Target, Speed * Time.deltaTime);			
            Player.transform.position = Destination;
        }
    }
}
