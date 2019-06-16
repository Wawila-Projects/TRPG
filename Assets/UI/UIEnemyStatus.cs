using Assets.EnemySystem;
using Assets.GameSystem;
using TMPro;
using UnityEngine;

namespace Assets.UI {
    public class UIEnemyStatus : MonoBehaviour {
        public TextMeshPro HP;
        public TextMeshPro StatusCondition;
        public GameObject Anchor;
        public bool isShowing;

        void Awake() {
            gameObject.SetActive(false);
        }

        public static UIEnemyStatus Initialize (string hp, string condition, GameObject anchor) {
            var floatingText = GameObject.Instantiate (UiManager.UI.DamageText);
            var component = floatingText.GetComponent<UIEnemyStatus> ();
            component.Show (hp, condition, anchor);
            return component;
        }
        public void Show (Enemy enemy) {
            if (enemy == null) {
                return;
            }

            var lifePercentage = Mathf.RoundToInt((float)enemy.CurrentHP/enemy.Hp * 100f);
            Show( $"{lifePercentage}%",
                $"{enemy.StatusEffect.CurrentEffect}",
                enemy.gameObject
            );
        }

        public void Show (string hp, string condition, GameObject anchor) {
            if (isShowing) {
                return;
            }

            isShowing = true;

            HP.text = hp;
            StatusCondition.text = condition;

            gameObject.transform.SetParent (anchor.transform);
            transform.position = anchor.transform.position + new Vector3 (1f, 2.5f, 0f);
            gameObject.SetActive(true);
        }

        public void Hide () {
            gameObject.transform.SetParent(GameController.Manager.transform);
            isShowing = false;
            gameObject.SetActive(false);
        }
    }
}