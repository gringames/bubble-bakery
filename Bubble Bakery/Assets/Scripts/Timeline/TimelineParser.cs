using System;
using UnityEngine;

namespace Timeline
{
    public class TimelineParser : MonoBehaviour
    {
        
        [Header("Actions")]
        [SerializeField] private EnterAction enterAction;
        [SerializeField] private ExitAction exitAction;
        [SerializeField] private OrderAction orderAction;
        [SerializeField] private TalkAction talkAction;
        
        [Header("File")]
        [SerializeField] private TextAsset timelineFile;
        [SerializeField] private string commentCharacter = "#";
        private string[] _lines;

        // ACTIONS
        private const string ENTER = "ENTER"; 
        private const string TALK = "TALK"; 
        private const string EXIT = "EXIT"; 
        private const string ORDER = "ORDER"; 
        
        // OBJECTS


        private void Awake()
        {
            InitializeTimeline();
            ParseTimeLine();
        }

        private void InitializeTimeline()
        {
            _lines = SplitTextAtNewLine(timelineFile.text);
        }
    
        private static string[] SplitTextAtNewLine(string text)
        {
            return text.Split(new[] {Environment.NewLine}, 
                StringSplitOptions.RemoveEmptyEntries);
        }

        private void ParseTimeLine()
        {
            foreach (var line in _lines)
            {
                Debug.Log("current line = " + line);
            
                // skip line if it is a comment
                if (line.StartsWith(commentCharacter)) continue;
            
                HandleLine(line);
            }
        }

        private void HandleLine(string line)
        {
            string[] contents = line.Split(" ");

            var action = contents[0];
            var arguments = GetArguments(contents);
                
            switch (action)
            {
                case ENTER: 
                    enterAction.Handle(arguments);
                    break;
                case EXIT:
                    exitAction.Handle(arguments);
                    break;
                case ORDER:
                    orderAction.Handle(arguments);
                    break;
                case TALK:
                    talkAction.Handle(arguments);
                    break;
            }
        }

        private static string[] GetArguments(string[] contents)
        {
            string[] rest = new string[contents.Length - 1];
            if (rest.Length == 0) return rest;
            
            for (int i = 1; i < contents.Length; i++)
            {
                rest[i - 1] = contents[i];
            }

            return rest;
        }
    }
}