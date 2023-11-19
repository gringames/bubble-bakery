using System;
using System.Collections.Generic;
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
        [SerializeField] private string splitCharacter = "§";
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

            Debug.Log("line index: " + _lineIndex);
            var line = _lines[_lineIndex];
            Debug.Log("current line = " + line);
            _lineIndex++;

            // skip line if it is a comment
            if (SkipLine(line))
            {
                ParseNextLine();
            }
            else
            {
                HandleLine(line);
            }
        }

        private bool SkipLine(string line)
        {
            return line.StartsWith(commentCharacter)
                   || line.StartsWith(" ")
                   || line.Length == 0;
        }

        private void HandleLine(string line)
        {
            string[] contents = line.Split(splitCharacter);

            var action = contents[0];
            var arguments = GetArguments(contents);
            
            Debug.Log($"action: {action}");
            string a = "";
            foreach (var arg in arguments)
            {
                a += arg + ", ";
            }
            Debug.Log($"with arguments: {a}");

            switch (action)
            {
                case ENTER:
                    Debug.Log("case enter");
                    arguments = AddTypeToMoveActionArguments(arguments, true);
                    moveAction.Handle(arguments);
                    break;
                case EXIT:
                    Debug.Log("case exit");
                    arguments = AddTypeToMoveActionArguments(arguments, false);
                    moveAction.Handle(arguments);
                    break;
                case ORDER:
                    Debug.Log("case order");
                    orderAction.Handle(arguments);
                    break;
                case TALK:
                    Debug.Log("case talk");
                    arguments = ConvertAllTalkLinesToArguments(arguments);
                    talkAction.Handle(arguments);
                    break;
                default:
                    Debug.LogError($"unknown action: {action}");
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

        private string[] ConvertAllTalkLinesToArguments(string[] currentArguments)
        {
            List<string> talksAsList = new List<string>();

            var characterName = currentArguments[0];
            var content = currentArguments[1];

            talksAsList.Add(characterName);
            talksAsList.Add(content);

            var line = _lines[_lineIndex];

            while (line.StartsWith(TALK))
            {
                var splits = line.Split(splitCharacter);

                // a talk line is comprised of TALK-name-content, we omit TALK here, as it is shared by all steps
                talksAsList.Add(splits[1]);
                talksAsList.Add(splits[2]);

                _lineIndex++;
                line = _lines[_lineIndex];
            }
            
            Debug.Log($"talk lines were added. Line index is now: {_lineIndex}");


            return talksAsList.ToArray();
        }
    }
}