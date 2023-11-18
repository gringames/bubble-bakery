using System.Collections.Generic;
using UnityEngine;

namespace Timeline
{
    public class EnterAction : MonoBehaviour, IAction
    {
        [Header("Characters")] [SerializeField]
        private static MovingObject Walrus;
        private static MovingObject Shark;
        private static MovingObject Cthullu;
        private static MovingObject Mereperson;
        // TODO: add other characters here + in the dictionary

        private readonly Dictionary<string, MovingObject> _nameToCharacterObject
            = new()
            {
                {"walrus", Walrus},
                {"shark", Shark},
                {"cthullu", Cthullu},
                {"mereperson", Mereperson}
            };


        /*
         * ARGUMENTS:
         * - name: the character's name -> store them here
         * - OPTIONAL exit point override (add later maybe)
         */
        public void Handle(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Debug.LogError("too few arguments for EnterAction provided!");
                return;
            }
            
            string name = arguments[0];
        }
    }
}