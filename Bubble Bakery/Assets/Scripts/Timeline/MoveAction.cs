using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timeline
{
    public class MoveAction : MonoBehaviour, IAction
    {
        [Header("Timeline")] [SerializeField] private TimelineParser timelineParser;

        [Header("Characters")] [SerializeField]
        private MovingObject walrus;

        [SerializeField] private MovingObject jean;
        [SerializeField] private MovingObject coral;
        [SerializeField] private MovingObject cthullu;
        [SerializeField] private MovingObject cthullusChildren;
        [SerializeField] private MovingObject cthulluAndChildren;
        [SerializeField] private MovingObject seaHorse1;
        [SerializeField] private MovingObject seaHorse2;
        [SerializeField] private MovingObject seaHorse3;
        [SerializeField] private MovingObject seaHorse4;

        [Header("Points")] [SerializeField] private Transform entryPoint;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private Transform exitPoint;

        public const string MoveIn = "IN";
        public const string MoveOut = "OUT";


        private MovingObject _currentCharacter;


        /*
         * ARGUMENTS:
         * - name: the character's name -> store them here
         * - type: IN/OUT -> side the object should move to
         */
        public void Handle(string[] arguments)
        {
            if (arguments.Length < 2)
            {
                Debug.LogError("too few arguments for EnterAction provided!");
                return;
            }

            int typeIndex = 0;
            var characters = new List<MovingObject>();


            for (int i = 0; i < arguments.Length - 1; i++)
            {
                string characterName = arguments[i].ToLower();
                var character = GetCharacterToName(characterName);
                characters.Add(character);

                typeIndex++;
            }

            // only subscribe to one (first) character
            _currentCharacter = characters[0];
            _currentCharacter.OnFinishedMoving += InformTimelineToGoOn;

            string type = arguments[typeIndex];

            MoveAll(characters, type);
        }

        private MovingObject GetCharacterToName(string characterName)
        {
            return characterName switch
            {
                "don" => walrus,
                "cthullu" => cthullu,
                "cthulluandchildren" => cthulluAndChildren,
                "cthulluschildren" => cthullusChildren,
                "coral" => coral,
                "jean" => jean,
                "s1" => seaHorse1,
                "s2" => seaHorse2,
                "s3" => seaHorse3,
                "s4" => seaHorse4,
                _ => null
            };
        }

        private void MoveAll(List<MovingObject> characters, string type)
        {
            if (type == MoveIn)
            {
                foreach (var character in characters)
                {
                    character.Enter(entryPoint, targetPoint);
                }
            }
            else
            {
                foreach (var character in characters)
                {
                    character.Exit(exitPoint);
                }
            }


        }

        private void InformTimelineToGoOn()
        {
            _currentCharacter.OnFinishedMoving -= InformTimelineToGoOn;
            timelineParser.ParseNextLine();
        }
    }
}