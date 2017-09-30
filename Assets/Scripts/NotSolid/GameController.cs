using UnityEngine;

namespace Assets.Scripts.NotSolid
{
    public class GameController : MonoBehaviour
    {
    
        public int Index;
        public Movement Movement;

        void Awake()
        {
            StaticInfo.Init ();
        }

        void Update ()
        {
            if (Input.GetMouseButtonUp (1))
                StaticInfo.TargetPlayer = null;

            StaticInfo.IsTargeting = Input.GetKey (KeyCode.Space);

            IterateThroughPlayerList ();
        }


        private void IterateThroughPlayerList()
        {
		
            Index = StaticInfo.CurrentParty.IndexOf (StaticInfo.SelectedPlayer);

            if (Index == -1)
                Index = 0;

            if (Input.GetKeyDown (KeyCode.Tab)) {
                if (Input.GetKey (KeyCode.RightShift) || Input.GetKey (KeyCode.LeftShift))
                    Index--;
                else
                    Index++;
			
                if (Index < 0)
                    Index = StaticInfo.CurrentParty.Count-1;
                if (Index > StaticInfo.CurrentParty.Count-1)
                    Index = 0;


                StaticInfo.SelectedPlayer = StaticInfo.CurrentParty [Index];
                Movement.Target = StaticInfo.SelectedPlayer.transform.position;
            }

        }
    }
}

