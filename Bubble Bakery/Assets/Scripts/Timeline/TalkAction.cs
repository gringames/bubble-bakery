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

            for (int i = 0; i < arguments.Length; i += 2)
            {
                Debug.Log(arguments[i] + ": " + arguments[i + 1]);
            }

            dialogueManager.ShowPanel();

            string characterName = arguments[0];
            dialogueManager.SetName(characterName);

            string dialogueText = arguments[1];
            dialogueManager.SetContent(dialogueText);

            // dialogueManager.HidePanel();
        }


        private void InformTimelineToGoOn()
        {
            timelineParser.ParseNextLine();
        }
    }
}