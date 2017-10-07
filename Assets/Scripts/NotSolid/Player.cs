using Assets.Scripts.Interfaces.Characters;
using UnityEngine;

namespace Assets.Scripts.NotSolid
{
    public class Player : MonoBehaviour, ICharacter {

        //void Start()
        //{
        //    if(CompareTag("Player"))
        //        StaticInfo.CurrentParty.Add (this);
        //}
		
        void OnMouseOver()
        {
            if (this == StaticInfo.TargetPlayer)
                StaticInfo.MouseOverTargetPlayer = true;

            if(CanTargetPlayer()) {
                if (StaticInfo.IsTargeting) 
                    StaticInfo.TargetPlayer = this;
			
                if (this != StaticInfo.SelectedPlayer)
                    StaticInfo.ClickedUnselectedUnit = true;
            }

            if (this.CompareTag ("Player"))
                PlayerAction ();
        }

        void PlayerAction()
        {
            if (this == StaticInfo.SelectedPlayer)
                StaticInfo.MouseOverSelectedPlayer = true;

            if (StaticInfo.SelectedPlayer == null && Input.GetMouseButton (0)) {
                StaticInfo.SelectedPlayer = this;
                StaticInfo.TargetPlayer = null;
            }

        }

        bool CanTargetPlayer()
        {
            bool selectedPlayerExist = false, correctInput = false;

            if (StaticInfo.SelectedPlayer != null)
                selectedPlayerExist = true;

            if (Input.GetMouseButton (0))
                correctInput = true;

            return selectedPlayerExist && correctInput;
        }

        void OnMouseExit()
        {
            StaticInfo.ClickedUnselectedUnit = false;
            StaticInfo.MouseOverTargetPlayer = false;
            StaticInfo.MouseOverSelectedPlayer = false;
        }
		
    }
}
