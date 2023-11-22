﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Timeline
{
    public class TimelineParser : MonoBehaviour
    {
        [SerializeField] private int nextSceneIndex = 2;

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
        private const string STOP = "STOP";

        // PARSING
        private int _lineIndex = 0;
        private int _lineCount;


        private void Awake()
        {
            InitializeTimeline();

            if (_lines[0] == STOP) return;

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
            if (_lineIndex >= _lineCount)
            {
                Debug.Log("end of timeline reached");
                SceneManager.LoadScene(nextSceneIndex);
                return;
            }

            var line = _lines[_lineIndex];
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
            if (action == STOP) return;

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

            while (LineIsValidForTalkAction(line))
            {
                if (line.StartsWith(TALK))
                {
                    var splits = line.Split(splitCharacter);

                    // a talk line is comprised of TALK-name-content, we omit TALK here, as it is shared by all steps
                    talksAsList.Add(splits[1]);
                    talksAsList.Add(splits[2]);
                }

                _lineIndex++;
                if (_lineIndex >= _lineCount)
                {
                      break;
                }
                line = _lines[_lineIndex];
            }

            return talksAsList.ToArray();
        }

        private bool LineIsValidForTalkAction(string line)
        {
            return line.StartsWith(TALK) || SkipLine(line);
        }
    }
}