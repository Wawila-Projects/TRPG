using Assets.Take_II.Scripts.Enums;
using UnityEngine;
using System;

namespace Assets.Take_II.Scripts.Spells {
    [Serializable]
    public class Spell {
        public string Id;
        public string Name;
        public string Description;
        public int Power;
        public float Cost;
        public bool isPhysical;
        public Enums.Elements Element; 
    
        public void Encode() {

    }
    }
}