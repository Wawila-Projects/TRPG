using System;
using System.Collections.Generic;
using Assets.Enums;
using Assets.CharacterSystem;
using Assets.CombatSystem;
using Assets.PlayerSystem;
using Assets.Spells;
using Assets.UI;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.SpellCastingSystem {
    public class SpellCasting<T> where T : Character {
        private Random random = new Random ();
        private const double ElementalAilmentChance = 0.1d;

        public bool CastSpell (SpellBase spell, T caster, List<Character> targets) {
            if (!spell.CanBeCasted(caster)) {
                return false;
            }

            var oneMore = false;
            caster.DeactivateOneMore();

            switch (spell) {
                case OffensiveSpell offensiveSpell:
                    oneMore = CastOffensiveSpell (offensiveSpell, caster, targets);
                    break;
                case AilementSpell ailemenntSpell:
                    CastAilementSpell (ailemenntSpell, caster, targets);
                    break;
                case RecoverySpell _:
                    if (spell is IHealingSpell healingSpell) {
                        CastHealingSpell (healingSpell, caster, targets);
                    }
                    if (spell is IReviveSpell reviveSpell && targets[0] is T character) {
                        CastReviveSpell (reviveSpell, character);
                    }
                    if (spell is IAssitSpell assistSpell) {
                        CastAssistSpell (assistSpell, targets, spell.Name);
                    }
                    break;
            }

            spell.HandleCostReduction (caster);

            if (oneMore) {
                caster.AddOneMore();
                return true;
            }

            caster.TurnFinished = true;
            return true;
        }

        private void CastAilementSpell (AilementSpell spell, T caster, List<Character> targets) {
            foreach (var target in targets) {
                var modifier = GetAilmentResistanceModifier (target);
                if (modifier == 0) continue;

                if (!CombatManager.SpellDidHit (caster, target, spell.HitChange * modifier))
                    continue;
                target.StatusEffect.SetStatusEffect (spell.StatusConditionInflicted);
            }
        }

        private void CastAssistSpell (IAssitSpell spell, List<Character> targets, string name) {
            foreach (var target in targets) {
                foreach (var statusConditoin in spell.CureableStatusConditions) {
                    target.StatusEffect.RemoveStatusEffect (statusConditoin);
                }
                UIFloatingText.Create(name, target.gameObject, Elements.Recovery);
            }
        }
        private void CastReviveSpell (IReviveSpell spell, T target) {
            if (!target.IsDead) return;

            target.CurrentHP = (int) Math.Ceiling (target.Hp * spell.PercentageLifeRecovered);
            target.IsDead = false;
            target.transform.position = target.Location.transform.position;

            UIFloatingText.Create($"{target.Name} revived!", target.gameObject, Elements.Recovery);
        }

        private void CastHealingSpell (IHealingSpell spell, T caster, List<Character> targets) {
            if (spell.FullHeal) {
                targets.ForEach (t => {
                    t.CurrentHP = t.Hp;
                    UIFloatingText.Create($"+{t.Hp}", t.gameObject, Elements.Recovery);
                });
                return;
            }

            foreach (var target in targets) {
                var amount = spell.HealingPower * CombatManager.PowerVariance (caster.Persona.Luck);
                target.CurrentHP += (int) Math.Ceiling (amount);
                UIFloatingText.Create($"+{(int) Math.Ceiling (amount)}", target.gameObject, Elements.Recovery);
            }
        }

        private bool CastOffensiveSpell (OffensiveSpell spell, T caster, List<Character> targets) {
            var blockModifiers = new List<ResistanceModifiers> () {
                ResistanceModifiers.Block, ResistanceModifiers.Absorb, ResistanceModifiers.Reflect
            };

            var oneMore = false;

            foreach (var target in targets) {
                oneMore = CombatManager.Manager.SpellAttack(caster, target, spell);

                 if (oneMore) {
                    if (target.StatusEffect == StatusConditions.Down) {
                        target.StatusEffect.SetStatusEffect(StatusConditions.Dizzy);
                        oneMore = false;
                    } else {
                        target.StatusEffect.SetStatusEffect(StatusConditions.Down);
                        oneMore = true;
                    }
                }

                var resistance = target.Persona.Resistances[spell.Element];
                if (blockModifiers.Contains (resistance)) continue;

                var modifier = GetAilmentResistanceModifier (target);
                if (modifier == 0) continue;
                if (random.NextDouble () > (ElementalAilmentChance * modifier)) continue;

                switch (spell.Element) {
                    case Elements.Fire:
                        target.StatusEffect.SetStatusEffect (StatusConditions.Burn);
                        continue;
                    case Elements.Ice:
                        target.StatusEffect.SetStatusEffect (StatusConditions.Freeze);
                        continue;
                    case Elements.Elec:
                        target.StatusEffect.SetStatusEffect (StatusConditions.Shock);
                        continue;
                    default:
                        continue;
                }
            }

            return oneMore;
        }

        private float GetAilmentResistanceModifier (Character target) {
            switch (target.Persona.Resistances[Elements.Ailment]) {
                case ResistanceModifiers.Weak:
                    return 2f;
                case ResistanceModifiers.Resist:
                    return 0.5f;
                case ResistanceModifiers.None:
                    return 1f;
                default:
                    return 0f;
            }
        }

    }
}