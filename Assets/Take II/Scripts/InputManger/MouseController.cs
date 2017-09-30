using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Take_II.Scripts.InputManger
{
    public class MouseController : MonoBehaviour
    {
        public GameObject Current;
        public GameObject Target;


        public void WindowsMouseManager()
        {
            //if (EventSystem.current.IsPointerOverGameObject())
            //    return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Current = hitInfo.collider.transform.parent.gameObject;

                Debug.Log(Current.name);


                if (Current.GetComponent<Tile>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var sprite = Current.GetComponentInChildren<SpriteRenderer>();
                        sprite.material.color = Color.red;
                    }
                }
                else if (Current.GetComponent<Player>() != null)
                {

                }
            }
        }
    }
}