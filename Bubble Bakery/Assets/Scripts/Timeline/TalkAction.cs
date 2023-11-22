using System;
using Dialogues;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Timeline
{
    public class TalkAction : MonoBehaviour, IAction
    {
        [Header("Timeline")] [SerializeField] private TimelineParser timelineParser;

        [Header("Dialogue Properties")] [SerializeField]
        private DialogueManager dialogueManager;


        private string[] _namesAndDialogues;
        private int _nameIndex = 0;

        private bool _dialogueIsFinished;
        private bool _dialogue;

        private void OnEnable()
        {
            dialogueManager.OnFinishedTyping += SetFinishedToTrue;
        }

        public void Handle(string[] arguments)
        {
            if (arguments.Length % 2 != 0)
            {
                Debug.LogError("wrong number of arguments for TALK action! Must be even.");
                return;
            }

            _dialogue = true;

            _namesAndDialogues = arguments;
            
            dialogueManager.ShowPanel();
            DisplayNextDialoguePart();
        }

        private void DisplayNextDialoguePart()
        {
            if (_nameIndex >= _namesAndDialogues.Length)
            {
                ResetDialogue();
                InformTimelineToGoOn();
            }
            
            DisplayName(_namesAndDialogues[_nameIndex]);
            AnimateDialogueText(_namesAndDialogues[_nameIndex + 1]);

            _nameIndex += 2;
        }

        private void DisplayName(string characterName)
        {
            dialogueManager.SetName(characterName);
        }

        private void AnimateDialogueText(string dialogueText)
        {
            _dialogueIsFinished = false;
            dialogueManager.AnimateContent(dialogueText);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseDown();
            }
        }

        private void MouseDown()
        {
            if (!_dialogue) return;
            if (_dialogueIsFinished) DisplayNextDialoguePart();
            else dialogueManager.SkipTyping();
        }

        private void SetFinishedToTrue()
        {
            _dialogueIsFinished = true;
        }


        private void ResetDialogue()
        {
            dialogueManager.HidePanel();
            _nameIndex = 0;
            _dialogueIsFinished = false;
            _dialogue = false;
        }
        
        private void InformTimelineToGoOn()
        {
            timelineParser.ParseNextLine();
        }
        
        private void OnDisable()
        {
            dialogueManager.OnFinishedTyping -= SetFinishedToTrue;
        }
    }
}