using Timeline;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string charName;

        [Header("Sounds")] [SerializeField] private AudioClip signatureSound;
        [SerializeField] private AudioClip happySound;
        [SerializeField] private AudioClip grumpySound;

        [Header("Actions")] [SerializeField] private MoveAction moveAction;
        [SerializeField] private TalkAction talkAction;
        [SerializeField] private OrderAction orderAction;

        public void Move()
        {
            if (moveAction is null)
            {
                Debug.LogError($"Character {charName} has no MoveAction assigned, therefore it cannot move!");
                return;
            }

            // TODO: process ENTER and EXIT correctly
            moveAction.Handle(null);
        }

        public void Talk()
        {
            if (talkAction is null)
            {
                Debug.LogError($"Character {charName} has no TalkAction assigned, therefore it cannot say anything!");
                return;
            }
        }

        public void Order()
        {
            if (orderAction is null)
            {
                Debug.LogError(
                    $"Character {charName} has no OrderAction assigned, therefore it cannot order anything!");
                return;
            }
        }
    }
}