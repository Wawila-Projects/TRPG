using System.Collections.Generic;

namespace Assets.PlayerManager
{
    public class ActionHandler {
        public int ActionPoints;
        public int CurrentActionPoints;
        private int MaxActionPoints;
        public bool MovementAvailable { get; private set; }

        public ActionHandler(int points = 3, int max = 5) {
            ActionPoints = points;
            CurrentActionPoints = ActionPoints;
            MaxActionPoints = max;
            MovementAvailable = true;
        }

        public void MoveActions() {
            --CurrentActionPoints;
            MovementAvailable = false;
        }

        public void AddMoreMovement() {
            CurrentActionPoints += 2;
            MovementAvailable = true;
        }

        // public bool AbilityAction(Ability ability) { 
        //     if (CurrentActionPoints < ability.ActionCost)
        //         return false;
            
        //     CurrentActionPoints -= ability.ActionCost;
        //     return true;
        // }

        public void HoldAction() {
            var pointsLeft = CurrentActionPoints;
            EndTurn();
            CurrentActionPoints += pointsLeft;

            CurrentActionPoints = CurrentActionPoints > MaxActionPoints ? 
                                    MaxActionPoints : CurrentActionPoints;
        }

        public void EndTurn() {
            CurrentActionPoints = ActionPoints;
            MovementAvailable = true;
        }
    }
}