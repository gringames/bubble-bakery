using Dialogues;
using Input;
using UnityEngine;

namespace Timeline
{
    public class TalkAction : MonoBehaviour, IAction
    {
        [Header("Timeline")] [SerializeField] private TimelineParser timelineParser;
        [Header("Input")] [SerializeField] private InputHandler inputHandler;

        [Header("Dialogue Properties")] [SerializeField]
        private DialogueManager dialogueManager;


        private string[] _namesAndDialogues;
        private int _nameIndex = 0;

        private bool _dialogueIsFinished;

        private void OnEnable()
        {
            dialogueManager.OnFinishedTyping += SetFinishedToTrue;
        }

        public void Handle(string[] arguments)
        {
            Debug.Log("entered dialogue handling");
            if (arguments.Length % 2 != 0)
            {
                Debug.LogError("wrong number of arguments for TALK action! Must be even.");
                return;
            }

            _namesAndDialogues = arguments;
            
            SetActionMap();

            dialogueManager.ShowPanel();
            DisplayNextDialoguePart();
        }

        private void SetActionMap()
        {
            inputHandler.ChangeInputMapTo("Dialogue");
        }
        
        private void ResetActionMap()
        {
            inputHandler.ChangeInputMapTo("Default");
        }

        private void DisplayNextDialoguePart()
        {
            if (_nameIndex >= _namesAndDialogues.Length)
            {
                Debug.Log("dialogue ended.");
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

        
        public void HandleMouseClick()
        {
            if (_dialogueIsFinished) DisplayNextDialoguePart();
            else dialogueManager.SkipTyping();
        }

        private void SetFinishedToTrue()
        {
            _dialogueIsFinished = true;
        }


        private void ResetDialogue()
        {
            ResetActionMap();
            dialogueManager.HidePanel();
            _nameIndex = 0;
            _dialogueIsFinished = false;
        }
        
        private void InformTimelineToGoOn()
        {
            Debug.Log("parser, go on pls");
            timelineParser.ParseNextLine();
        }
        
        private void OnDisable()
        {
            dialogueManager.OnFinishedTyping -= SetFinishedToTrue;
        }
    }
}