using System;
using System.Collections.Generic;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class ResistanceManager
    {
        private readonly Dictionary<Enums.Elements, int> _resistances;

        public ResistanceManager()
        {
            _resistances = new Dictionary<Enums.Elements, int>
            {
                {Enums.Elements.Fire, 0 },
                {Enums.Elements.Water, 0 },
                {Enums.Elements.Earth, 0 },
                {Enums.Elements.Wind, 0 },
                {Enums.Elements.Lightining, 0 },
                {Enums.Elements.Ice, 0 }
                
            };
        }

        public int FireResistance { get { return _resistances[Enums.Elements.Fire]; } }
        public int WaterResistance { get { return _resistances[Enums.Elements.Water]; } }
        public int EarthResistance { get { return _resistances[Enums.Elements.Earth]; } }
        public int WindResistance { get { return _resistances[Enums.Elements.Wind]; } }
        public int LightningResistance { get { return _resistances[Enums.Elements.Lightining]; } }
        public int IceResistance { get { return _resistances[Enums.Elements.Ice]; } }

        public int ResistedDamage(int damage, Enums.Elements element)
        {
            if (element == Enums.Elements.None)
                return damage;

            var resistance = _resistances[element];

            if (resistance == 0)
                return damage;

            if (resistance == 100)
                return 0;

            var resistedDamage = (int) Math.Floor((decimal)(damage * (resistance / 100)));

            if (resistance < 100)
            {    
                return (int) Math.Floor((decimal) (damage - resistedDamage));
            }

            return -resistedDamage;
        }

        public void ModifyResistance(Enums.Elements element, int change)
        {
            if(element == Enums.Elements.None)
                return;

            _resistances[element] += change;
        }
    }
}