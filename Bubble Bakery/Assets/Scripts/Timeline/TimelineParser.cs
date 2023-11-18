using System;
using UnityEngine;

namespace Timeline
{
    public class TimelineParser : MonoBehaviour
    {
        [Header("Actions")] [SerializeField] private MoveAction moveAction;
        [SerializeField] private OrderAction orderAction;
        [SerializeField] private TalkAction talkAction;

        [Header("File")] [SerializeField] private TextAsset timelineFile;
        [SerializeField] private string commentCharacter = "#";
        private string[] _lines;

        // ACTIONS
        private const string ENTER = "ENTER";
        private const string TALK = "TALK";
        private const string EXIT = "EXIT";
        private const string ORDER = "ORDER";

        // PARSING
        private int _lineIndex = 0;
        private int _lineCount;


        private void Awake()
        {
            InitializeTimeline();
            ParseNextLine();
        }

        private void InitializeTimeline()
        {
            _lines = SplitTextAtNewLine(timelineFile.text);
            _lineCount = _lines.Length;
        }

        private static string[] SplitTextAtNewLine(string text)
        {
            return text.Split(new[] {Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);
        }

        public void ParseNextLine()
        {
            if (_lineIndex == _lineCount)
            {
                Debug.Log("end of timeline reached");
                return;
                // TODO: display end scene
            }

            var line = _lines[_lineIndex];
            _lineIndex++;

            Debug.Log("current line = " + line);

            // skip line if it is a comment
            if (SkipLine(line)) ParseNextLine();

            HandleLine(line);
        }

        private bool SkipLine(string line)
        {
            return line.StartsWith(commentCharacter)
                   || line.StartsWith(" ")
                   || line.Length == 0;
        }

        private void HandleLine(string line)
        {
            string[] contents = line.Split(" ");

            var action = contents[0];
            var arguments = GetArguments(contents);

            switch (action)
            {
                case ENTER:
                    arguments = AddTypeToMoveActionArguments(arguments, true);
                    moveAction.Handle(arguments);
                    break;
                case EXIT:
                    arguments = AddTypeToMoveActionArguments(arguments, false);
                    moveAction.Handle(arguments);
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

        private static string[] AddTypeToMoveActionArguments(string[] originalArguments, bool isEnter)
        {
            int originalLength = originalArguments.Length;

            string[] newArguments = new string[originalLength + 1];

            for (int i = 0; i < originalLength; i++)
            {
                newArguments[i] = originalArguments[i];
            }

            string type = isEnter
                ? MoveAction.MoveIn
                : MoveAction.MoveOut;

            newArguments[originalLength] = type;

            return newArguments;
        }
    }
}