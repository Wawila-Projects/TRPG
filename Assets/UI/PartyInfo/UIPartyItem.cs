using Assets.PlayerSystem;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.UI.PartyInfo {
    public class UIPartyItem: MonoBehaviour {
        public Player Player;
        public RawImage Avatar;
        public Text LifeNumber;
        public Text ManaNumber;
        public Slider LifeBar;
        public Slider ManaBar;

        void Awake () {
            Avatar.color = Player.GetComponentInParent<Renderer> ().material.color;
            LifeBar.minValue = 0;
            LifeBar.maxValue = Player.Hp;
            ManaBar.minValue = 0;
            ManaBar.maxValue = Player.Sp;
        }

        void Update () {
            LifeNumber.text = $"{Player.CurrentHP}";
            ManaNumber.text = $"{Player.CurrentSP}";
            LifeBar.value = Player.CurrentHP;
            ManaBar.value = Player.CurrentSP;
        }

    }
}