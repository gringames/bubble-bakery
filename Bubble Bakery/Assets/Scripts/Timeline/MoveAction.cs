using UnityEngine;

namespace Timeline
{
    public class MoveAction : MonoBehaviour, IAction
    {
        [Header("Characters")] [SerializeField]
        private MovingObject walrus;

        [SerializeField] private MovingObject shark;
        [SerializeField] private MovingObject cthullu;
        [SerializeField] private MovingObject mereperson;

        [Header("Points")] [SerializeField] private Transform entryPoint;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private Transform exitPoint;

        public const string MoveIn = "IN";
        public const string MoveOut = "OUT";

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
            
            string characterName = arguments[0].ToLower();
            var character = GetCharacterToName(characterName);

            string type = arguments[1];
            if (type == MoveIn)
                character.Enter(entryPoint, targetPoint);
            else
                character.Exit(exitPoint);

            while (!character.finishedMoving)
            {
                Debug.Log("wait");
            }
        }

        private MovingObject GetCharacterToName(string characterName)
        {
            return characterName switch
            {
                "walrus" => walrus,
                "cthullu" => cthullu,
                "mereperson" => mereperson,
                "shark" => shark,
                _ => null
            };
        }
    }
}