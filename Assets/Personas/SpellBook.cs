﻿using System.Collections.Generic;
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
            Restrictions = ElementalRestrinctions[baseElement];
        }

        public SpellBook(PersonaBase owner, Elements baseElement, List<SpellBase> spells):
            this(owner, baseElement, spells, new Dictionary<int, SpellBase>()) {
        }
        public SpellBook(PersonaBase owner, Elements baseElement): 
            this(owner, baseElement, new List<SpellBase>(), new Dictionary<int, SpellBase>()) {
        }
        public (bool, SpellBase) LevelUp() {
            SpellBase spell = LockedSpells[Owner.Level];
            if (spell == null) return (true, null);

            var resolved = AddSpell(spell);
            if (!resolved)  return (false, null);
            
            LockedSpells.Remove(Owner.Level);
            return (true, spell);
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

        public static Dictionary<Elements, List<Elements>> ElementalRestrinctions = new Dictionary<Elements, List<Elements>>{
            {Elements.Elec, new List<Elements> { Elements.Wind }},
            {Elements.Wind, new List<Elements> { Elements.Elec }},
            {Elements.Fire, new List<Elements> { Elements.Ice }},
            {Elements.Ice, new List<Elements> { Elements.Fire }},
            {Elements.Psy, new List<Elements> { Elements.Nuke }},
            {Elements.Nuke, new List<Elements> { Elements.Psy }},
            {Elements.Ailment,  new List<Elements> {
                Elements.Bless, Elements.Recovery
            }},
            {Elements.Recovery,  new List<Elements> {
                Elements.Physical, Elements.Curse
            }},
            {Elements.Curse,  new List<Elements> {
                Elements.Physical, Elements.Bless, Elements.Recovery
            }},
            {Elements.Bless,  new List<Elements> {
                Elements.Physical, Elements.Curse, Elements.Ailment
            }},
            {Elements.Physical, new List<Elements> {
                Elements.Fire, Elements.Ice, Elements.Elec, Elements.Wind, 
                Elements.Psy, Elements.Nuke, Elements.Bless, Elements.Curse
            }},
            {Elements.Almighty, new List<Elements>()},
            {Elements.None, new List<Elements> {
                Elements.Fire, Elements.Ice, Elements.Elec, Elements.Wind, 
                Elements.Psy, Elements.Nuke, Elements.Bless, Elements.Curse, 
                Elements.Physical, Elements.Ailment, Elements.Recovery, Elements.Almighty,
            }},
        };
    }
}