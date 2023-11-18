using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("TMP Objects")] [SerializeField]
        private GameObject dialoguePanel;

        [SerializeField] private TextMeshProUGUI dialogueName;
        [SerializeField] private TextMeshProUGUI dialogueText;


        [Header("Text Scrolling")] [SerializeField]
        private float timeBetweenLetters = .01f;

        private string _currentContent;

        public Action OnFinishedTyping;
        
        
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
            dialogueName.text = characterName;
        }

        
        public void AnimateContent(string content)
        {
            _currentContent = content;
            dialogueText.text = string.Empty;
            StartCoroutine(nameof(Type), content);
        }

        public void SkipTyping()
        {
            StopAllCoroutines();
            dialogueText.text = _currentContent;
            
            OnFinishedTyping?.Invoke();
        }

        private IEnumerator Type(string content)
        {
            foreach (var c in content.ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(timeBetweenLetters);
            }
            
            OnFinishedTyping?.Invoke();
        }

    }
}