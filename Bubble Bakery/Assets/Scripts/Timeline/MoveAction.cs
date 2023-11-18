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

        public static string MoveIN = "IN";
        public static string MoveOUT = "OUT";

        /*
         * ARGUMENTS:
         * - type: IN/OUT -> side the object should move to
         * - name: the character's name -> store them here
         */
        public void Handle(string[] arguments)
        {
            if (arguments.Length <= 1)
            {
                Debug.LogError("too few arguments for EnterAction provided!");
                return;
            }
            
            string characterName = arguments[1].ToLower();
            var character = GetCharacterToName(characterName);

            string type = arguments[0];
            if (type == MoveIN)
                character.Enter(entryPoint, targetPoint);
            else
                character.Exit(exitPoint);
        }

        private MovingObject GetCharacterToName(string characterName)
        {
            Debug.Log(characterName);
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