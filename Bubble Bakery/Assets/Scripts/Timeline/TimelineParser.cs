using System;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using Util;

namespace Timeline
{
    public class TimelineParser : MonoBehaviour
    {
        [Header("Timeline Input File")] [SerializeField] private TextAsset timelineFile;
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
                Logic.Instance.SceneChanger.ChangeScene();
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

            var characterName = contents[1];
            Character character = CharacterMap.Instance.GetCharacterToName(characterName);
            
            HandleAction(action, character, contents);
        }

        private static void HandleAction(string action, Character character, string[] contents)
        {
            if (character is null)
            {
                Debug.LogError($"invalid character name for action {action}!");
                return;
            }
            
            switch (action)
            {
                case ENTER:
                    character.Move(true);
                    break;
                case EXIT:
                    character.Move(false);
                    break;
                case ORDER:
                    character.Order(); // TODO: add params maybe?
                    break;
                case TALK:
                    // contents = { action, name, args }
                    if (contents.Length != 3)
                    {
                        contents.Print("invalid arguments for TALK action", true);
                        return;
                    }
                    character.Talk(contents[2]); // TODO: add params -> text
                    break;
                default:
                    Debug.LogError($"unknown action: {action}");
                    break;
            }
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