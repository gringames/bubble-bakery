using System.ComponentModel;
using TMPro;
using UnityEngine;
namespace Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("TMP Objects")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueName;
        [SerializeField] private TextMeshProUGUI dialogueText;


        [Header("Text Scrolling")] [SerializeField]
        private float scrollSpeed = 1;

        private bool _startScrolling;

        public void ShowPanel()
        {
            dialoguePanel.SetActive(true);
        }
        
        public void HidePanel()
        {
            dialoguePanel.SetActive(false);
        }
        public void SetName(string characterName)
        {
            Debug.Log($"name: {characterName}");
            Debug.Log($"tmp: {dialogueName.text}");
            dialogueName.text = characterName;
        }
        
        public void SetContent(string content)
        {
            dialogueText.text = content;
        }
    }
}