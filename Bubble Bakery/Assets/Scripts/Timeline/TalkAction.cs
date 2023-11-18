using System;
using Dialogues;
using UnityEngine;

namespace Timeline
{
    public class TalkAction : MonoBehaviour, IAction
    {
        [Header("Timeline")] [SerializeField] private TimelineParser timelineParser;

        [Header("Dialogue Properties")] [SerializeField]
        private DialogueManager dialogueManager;

        public void Handle(string[] arguments)
        {
            if (arguments.Length % 2 != 0)
            {
                Debug.LogError("wrong number of arguments for TALK action! Must be even.");
                return;
            }

            dialogueManager.ShowPanel();
            DisplayFirstDialoguePart(arguments);
            // TODO: listen to mouse click and traverse dialogue
        }

        private void DisplayFirstDialoguePart(string[] arguments)
        {
            string characterName = arguments[0];
            dialogueManager.SetName(characterName);

            string dialogueText = arguments[1];
            dialogueManager.SetContent(dialogueText);
        }


        private void InformTimelineToGoOn()
        {
            timelineParser.ParseNextLine();
        }
    }
}