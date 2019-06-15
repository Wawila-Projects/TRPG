using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.CharacterSystem;
using Assets.Enums;
using Assets.UI;
using Assets.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI {
    [RequireComponent (typeof (TextMeshPro))]
    class UIFloatingText : MonoBehaviour {
        private static Dictionary<GameObject, List<UIFloatingText>> ShowingText = new Dictionary<GameObject, List<UIFloatingText>> ();

        public TextMeshPro Text;
        public GameObject Anchor;
        public float FadeTime;
        public float ScrollSpeed;
        private float time;
        private bool ready;

        void Awake () {
            Text = GetComponent<TextMeshPro> ();
        }

        void Update () {
            if (!ready) {
                return;
            }

            time = Time.deltaTime / FadeTime;

            if (Text.alpha <= 0) {
                RemoveShowingText (Anchor, this);
                Destroy (gameObject);
                return;
            }
            var pos = transform.position;
            pos.y += ScrollSpeed * Time.deltaTime;
            transform.position = pos;

            Text.alpha -= time;
        }

        IEnumerator WaitXSeconds (float amount) {
            yield return new WaitForSeconds (amount);
            ready = CheckIfReady ();
            if (ready) {
                GetComponent<Renderer> ().enabled = true;
                yield return null;
                yield break;
            }

            var next = ShowingText[Anchor].First ();
            GetComponent<Renderer> ().enabled = true;
            next.ready = true;
            StartCoroutine (WaitXSeconds (0.3f));
        }

        public static UIFloatingText Create (string text, GameObject anchor, Elements? element = null) {
            var floatingText = GameObject.Instantiate (UiManager.UI.DamageText);
            var component = floatingText.GetComponent<UIFloatingText> ();
            component.init (text, anchor, element);

            return component;
        }

        private void init (string text, GameObject anchor, Elements? element = null) {
            gameObject.transform.SetParent (anchor.transform);

            var exists = ShowingText.TryGetValue (anchor, out var list);
            gameObject.name = $"FloatingText_{list?.Count ?? 0}";

            transform.position = anchor.transform.position + new Vector3 (-1.75f, 2f, 0f);

            Text.text = text;
            Anchor = anchor;

            if (element != null) {
                SetElementalColor (element ?? Elements.None);
            }

            ready = CheckIfReady ();
            AddShowingText (anchor, this);

            if (ready) {
                return;
            }

            GetComponent<Renderer> ().enabled = false;
            StartCoroutine (WaitXSeconds (0.3f));
        }

        private bool CheckIfReady () {
            if (ShowingText.ContainsKey (Anchor)) {
                var showing = ShowingText[Anchor];
                showing.Any (s => s.Text.alpha >= 0.8 && s != this);
                return false;
            }

            return true;
        }

        private void SetElementalColor (Elements element) {
            switch (element) {
                case Elements.Recovery:
                    Text.faceColor = Color.cyan;
                    break;
                case Elements.Ailment:
                    Text.outlineWidth = 0;
                    break;
                case Elements.Fire:
                    Text.faceColor = Color.red;
                    break;
                case Elements.Elec:
                    Text.faceColor = Color.yellow;
                    break;
                case Elements.Wind:
                    Text.faceColor = Color.green;
                    break;
                case Elements.Ice:
                    Text.faceColor = Color.blue;
                    break;
                default:
                    return;
            }
        }

        private static void AddShowingText (GameObject key, UIFloatingText value) {
            if (UIFloatingText.ShowingText.ContainsKey (key)) {
                UIFloatingText.ShowingText[key].Add (value);
                return;
            }
            UIFloatingText.ShowingText[key] = new List<UIFloatingText> () {
                value
            };
        }
        private static void RemoveShowingText (GameObject key, UIFloatingText value) {
            if (!UIFloatingText.ShowingText.ContainsKey (key)) {
                return;
            }

            UIFloatingText.ShowingText[key].Remove (value);

            if (!UIFloatingText.ShowingText[key].IsEmpty ()) {
                return;
            }

            UIFloatingText.ShowingText.Remove (key);
        }
    }
}