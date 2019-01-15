using System.Collections.Generic;
using Assets.Spells;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.GameManager;

namespace Assets.Personas {
    public class SpellBook {
        public Elements BaseElement { get; }
        public PersonaBase Owner { get; }
        public List<SpellBase> Spells { get; }
        public Dictionary<int, SpellBase> LockedSpells { get; }
        public List<Elements> Restrictions { get; private set; }

        public SpellBook(PersonaBase owner, Elements baseElement, 
                         List<SpellBase> spells, Dictionary<int, SpellBase> lockedSpells) {
            Owner = owner;
            BaseElement = baseElement;
            Spells = spells;
            LockedSpells = lockedSpells;
            Restrictions = GetRestrictions();
        }

        public SpellBook(PersonaBase owner, Elements baseElement, List<SpellBase> spells):
            this(owner, baseElement, spells, new Dictionary<int, SpellBase>()) {
        }
        public SpellBook(PersonaBase owner, Elements baseElement): 
            this(owner, baseElement, new List<SpellBase>(), new Dictionary<int, SpellBase>()) {
        }
        public bool LevelUp(out SpellBase spell) {
            spell = LockedSpells[Owner.Level];
            if (spell == null) return true;

            var resolved = AddSpell(spell);
            if (!resolved)  return false;
            
            LockedSpells.Remove(Owner.Level);
            return true;
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
                case Elements.Ailment:
                    return new List<Elements> {
                        Elements.Bless, Elements.Recovery
                    };
                case Elements.Recovery:
                    return new List<Elements> {
                        Elements.Physical, Elements.Curse
                    };
                case Elements.Curse:
                    return new List<Elements> {
                        Elements.Physical, Elements.Bless, Elements.Recovery
                    };
                case Elements.Bless: 
                    return new List<Elements> {
                        Elements.Physical, Elements.Curse, Elements.Ailment
                    };
                default: 
                    return new List<Elements>();
            }
        }
    }
}