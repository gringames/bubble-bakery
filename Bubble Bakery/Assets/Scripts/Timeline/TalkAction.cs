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
            if (arguments.Length < 2)
            {
                Debug.LogError("too few arguments for TALK action!");
                return;
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