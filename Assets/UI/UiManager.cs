using UnityEngine;
using UnityEngine.UI;

 namespace Assets.UI
 {
     public sealed class UiManager : MonoBehaviour
     {
        public static UiManager UI { get; private set; }

        public GameObject DamageText;

        public Canvas Canvas;

        void Awake() {
            UI = this;
        }
     }
 }