using System.Collections.Generic;
using Assets.Enums;
using Assets.Spells;
using Assets.GameSystem;
using Assets.PlayerSystem;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.EnemySystem {
    public class Hivemind {
        public static Hivemind Global { get; private set; } = new Hivemind ();
        public IDictionary<Player, HivemindInfo> CollectedInfo;

        public Hivemind (IDictionary<Player, HivemindInfo> startingInfo = null) {
            if (startingInfo != null) {
                CollectedInfo = startingInfo;
            } else {
                CollectedInfo = new Dictionary<Player, HivemindInfo>();
            }


            // TODO: Temporary Fix
            // foreach(var player in GameController.Manager.Players) {
            //     if (CollectedInfo.ContainsKey(player)) continue;
            //     try {
            //         CollectedInfo.Add(player, new HivemindInfo(player));
            //     } catch {
            //         continue;
            //     }
            // }
        }

        public void ResetHivemind() {
            CollectedInfo = new Dictionary<Player, HivemindInfo>();
            foreach(var player in GameController.Manager.Players) {
                CollectedInfo[player] = new HivemindInfo(player);
            }
        }

        public void CaptureInfoWhenAttacking (Player player, OffensiveSpell spell, int damage) {
            var info = CollectedInfo[player];
            info.LastSeenLocation = player.Location;
            info.HpLost += damage;
            info.Resistances[spell.Element] = player.Persona.Resistances[spell.Element];

            if (player.IsDead && !info.Death.isDead) {
                info.Death = (info.HpLost, true);
            }
        }

        public void CaptureInfoWhenBasicAttacking (Player player, int damage) {
            var info = CollectedInfo[player];
            info.LastSeenLocation = player.Location;
            info.Resistances[Elements.Physical] = player.Persona.Resistances[Elements.Physical];
            info.HpLost += damage;

            if (player.IsDead && !info.Death.isDead) {
                info.Death = (info.HpLost, true);
            }
        }
        public void CaptureInfoWhenBasicAttacked (Player player, int damage) {
            var info = CollectedInfo[player];
            info.LastSeenLocation = player.Location;
            info.Resistances[Elements.Physical] = player.Persona.Resistances[Elements.Physical];
    
            var (count, total, average) = info.BasicAttackDamage;
            var newTotal = total + damage;
            var newCount = count + 1;
            var newAvarage = newTotal / newCount;
            info.BasicAttackDamage = (newCount, newTotal, newAvarage);
        }

        public void CaptureInfoWhenAttacked (Player player, OffensiveSpell spell) {
            var info = CollectedInfo[player];
            info.LastSeenLocation = player.Location;
            info.Spells.Add(spell);

            if (spell.IsMagical) {
                info.SpLost += spell.Cost;
            }

            // else if (can see players max hp) {
            //     var cost = (int) Math.Ceiling(player.Hp * (spell.Cost/100f));
            // }
        }

        // TODO: Collect Info when giving status ailment
        public class HivemindInfo {
            public HivemindInfo(Player player) {
                Player = player;

                Spells = new HashSet<CastableSpell>();
                Resistances = new Dictionary<Elements, ResistanceModifiers>();
            }

            public Player Player;
            public int HpLost = 0;
            public int SpLost = 0;
            public (int diedAt, bool isDead) Death = (0, false);
            public (int count, int total, int average) BasicAttackDamage = (0,0,0);
            public Tile LastSeenLocation;
            public StatusConditions StatusEffect = StatusConditions.None;
            public HashSet<CastableSpell> Spells;
            public IDictionary<Elements, ResistanceModifiers> Resistances;
        }
    }

}