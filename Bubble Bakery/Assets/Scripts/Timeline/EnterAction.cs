using UnityEngine;

namespace Timeline
{
    public class EnterAction : MonoBehaviour, IAction
    {
        [Header("Characters")]
        [SerializeField] private MovingObject walrus;
        [SerializeField] private MovingObject shark;
        [SerializeField] private MovingObject cthullu;
        [SerializeField] private MovingObject mereperson;
        
        

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
            
            string name = arguments[0].ToLower();
            var character = GetCharacterToName(name);
            character.Enter();
        }

        private MovingObject GetCharacterToName(string name)
        {
            Debug.Log(name);
            return name switch
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