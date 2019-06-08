using System;
using System.Collections.Generic;
using Assets.CharacterSystem;
using Assets.CombatSystem;
using Assets.Enums;
using Assets.PlayerSystem;
using Assets.Spells;
using Asstes.CharacterSystem;

namespace Assets.SpellCastingSystem {
    public class SpellCasting {
        private Random random = new Random ();
        private const double ElementalAilmentChance = 0.1d;

        public void CastSpell (SpellBase spell, Player caster, List<Character> targets) {
            switch (spell) {
                case OffensiveSpell offensiveSpell:
                    CastOffensiveSpell (offensiveSpell, caster, targets);
                    break;
                case AilementSpell ailemenntSpell:
                    CastAilementSpell (ailemenntSpell, caster, targets);
                    break;
                case RecoverySpell _:
                    if (spell is IHealingSpell healingSpell) {
                        CastHealingSpell (healingSpell, caster, targets);
                    }
                    if (spell is IReviveSpell reviveSpell && targets[0] is Player player) {
                        CastReviveSpell (reviveSpell, player);
                    }
                    if (spell is IAssitSpell assistSpell) {
                        CastAssistSpell (assistSpell, targets);
                    }
                    break;
            }

            spell.HandleCostReduction (caster);
            caster.TurnFinished = true;
        }

        private void CastAilementSpell (AilementSpell spell, Player caster, List<Character> targets) {
            foreach (var target in targets) {
                if (!CombatManager.SpellDidHit (caster, target, spell.HitChange))
                    continue;

                target.StatusEffect.SetStatusEffect (spell.StatusConditionInflicted);
            }
        }

        private void CastAssistSpell (IAssitSpell spell, List<Character> targets) {
            foreach (var target in targets) {
                foreach (var statusConditoin in spell.CureableStatusConditions) {
                    target.StatusEffect.RemoveStatusEffect (statusConditoin);
                }
            }
        }
        private void CastReviveSpell (IReviveSpell spell, Player target) {
            if (!target.IsDead) return;

            target.CurrentHP = (int) MathF.Ceiling (target.Hp * spell.PercentageLifeRecovered);
            target.IsDead = false;

            UnityEngine.Debug.Log ($"{target.Name} revived!");
        }

        private void CastHealingSpell (IHealingSpell spell, Player caster, List<Character> targets) {
            if (spell.FullHeal) {
                targets.ForEach (t => {
                    t.CurrentHP = t.Hp;
                    UnityEngine.Debug.Log ($"{t.Name} fully healed!");
                    });
                return;
            }

            foreach (var target in targets) {
                var amount = spell.HealingPower * CombatManager.PowerVariance (caster.Persona.Luck);
                target.CurrentHP += (int) MathF.Ceiling (amount);

                UnityEngine.Debug.Log ($"{target.Name} healed for {amount}!");
            }
        }

        private void CastOffensiveSpell (OffensiveSpell spell, Player caster, List<Character> targets) {
            foreach (var target in targets) {
                CombatManager.Manager.SpellAttack (caster, target, spell);

                if (random.NextDouble () > ElementalAilmentChance) continue;

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
        }

    }
}