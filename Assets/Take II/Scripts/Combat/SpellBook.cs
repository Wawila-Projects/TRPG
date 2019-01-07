using System.Collections.Generic;
using Assets.Spells;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.GameManager;

namespace Assets.Take_II.Scripts.Combat {
    public class SpellBook {
        public Elements BaseElement { get; private set; }
        public Character Owner { get; private set; }
        public List<SpellBase> Spells { get; private set; }
        public List<Elements> Restrictions { get; private set; }

        SpellBook(Character owner, Elements baseElement, List<SpellBase> spells) {
            Owner = owner;
            BaseElement = baseElement;
            Spells = spells;
            Restrictions = GetRestrictions();
        }
        SpellBook(Character owner, Elements baseElement): 
            this(owner, baseElement, new List<SpellBase>()) {
        }

        public bool AddSpell(SpellBase spell) {
            var isAtSpellLimit = Spells.Count == 8;
            var alreadyHaveSpell = Spells.Contains(spell);
            var elementRestricted = Restrictions.Contains(spell.Element);
            if (isAtSpellLimit || alreadyHaveSpell || elementRestricted) 
            {
                return false;
            }

            var exclusiveSpell = spell as IExclusiveSpell;
            if (!exclusiveSpell?.ExclusiveUnits.Contains(Owner.Name) ?? false) {
                return false;
            }

            Spells.Add(spell);
            return true;
        }

        public bool DeleteSpell(SpellBase spell) {
            return Spells.RemoveAll((s) => s == spell) > 0;
        }

        private List<Elements> GetRestrictions() {
            switch(BaseElement) {
                case Elements.Elec:
                    return new List<Elements> { Elements.Wind };
                case Elements.Fire:
                    return new List<Elements> { Elements.Ice };
                case Elements.Ice:
                    return new List<Elements> { Elements.Fire };
                case Elements.Wind:
                    return new List<Elements> { Elements.Elec };
                case Elements.Ailement:
                    return new List<Elements> {
                        Elements.Light, Elements.Recovery
                    };
                case Elements.Recovery:
                    return new List<Elements> {
                        Elements.Physical, Elements.Darkness
                    };
                case Elements.Darkness:
                    return new List<Elements> {
                        Elements.Physical, Elements.Light, Elements.Recovery
                    };
                case Elements.Light: 
                    return new List<Elements> {
                        Elements.Physical, Elements.Darkness, Elements.Ailement
                    };
                default: 
                    return new List<Elements>();
            }
        }
    }
}