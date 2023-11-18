using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class DialogueHandler : MonoBehaviour
    {
        [Header("PopupDisplay")] [SerializeField]
        private Canvas textbox;

        [Header("TextScrolling")] [SerializeField]
        private TextMeshProUGUI textComponent;

        private bool _startScrolling;
    }
}

/*
 *

namespace DefaultNamespace
{
    // [UNUSED]
    public class Dialogue : MonoBehaviour
    {
        [Header("PopupDisplay")] [SerializeField]
        private Canvas textbox;

        [SerializeField] private List<Button> buttons;
        private bool _startScrolling;


        [Header("TextScrolling")] [SerializeField]
        private TextMeshProUGUI textComponent;

        //[SerializeField] private bool ControlsText;
        [SerializeField] private string[] lines;
        [SerializeField] private float timeBetweenTyping;
        private int _index;

        private void Awake()
        {
            textbox.enabled = false;
            _startScrolling = false;

            foreach (var b in buttons)
            {
                b.gameObject.SetActive(false);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            ShowTextbox();
            _startScrolling = true;
            StartDialogue();
        }


        private void ShowTextbox()
        {
            textbox.enabled = true;
        }


        public void HideTextbox()
        {
            textbox.enabled = false;
        }


        // [source for text scrolling:] https://www.youtube.com/watch?v=8oTYabhj248
        void Update()
        {
            if (!_startScrolling) return;

            // skip scrolling or move to next line
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            if (textComponent.text == lines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[_index];
            }
        }

        private void StartDialogue()
        {
            Debug.Log("Starting Dialogue");
            _index = 0;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        private IEnumerator TypeLine()
        {
            foreach (var c in lines[_index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(timeBetweenTyping);
            }

            Debug.Log("enabling Buttons");
            foreach (var b in buttons)
            {
                b.gameObject.SetActive(true);
            }
        }

        private void NextLine()
        {
            if (_index >= lines.Length - 1)
            {
                Debug.Log("finished displaying text");
                return;
            }

            _index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
}

 */