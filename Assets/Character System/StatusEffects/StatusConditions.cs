using Asstes.CharacterSystem.StatusEffects;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public static class StatusConditions {
        public static StatusEffect GetStatusCondition (StatusCondition statusCondition) {
            switch (statusCondition) {
                case StatusCondition.Freeze:
                    return new Freeze ();
                case StatusCondition.Burn:
                    return new Burn ();
                case StatusCondition.Shock:
                    return new Shock ();
                case StatusCondition.Sleep:
                    return new Sleep ();
                case StatusCondition.Rage:
                    return new Rage ();
                    // case StatusCondition.Fear:
                    //     return new Fear ();
                    // case StatusCondition.Confusion:
                    //     return new Confusion ();
                    // case StatusCondition.Charm:
                    //     return new Charm ();
                case StatusCondition.Exhaustion:
                    return new Exhaustion ();
                    // case StatusCondition.Silence:
                    //     return new Silence ();
                case StatusCondition.Enervation:
                    return new Freeze ();
                    // case StatusCondition.Distress:
                    //     return new Distress ();
                case StatusCondition.Dispair:
                    return new Dispair ();
                case StatusCondition.Down:
                    return new Down ();
                case StatusCondition.Dizzy:
                    return new Dizzy ();
            }

            return null;
        }
    }
}