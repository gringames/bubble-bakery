using Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        [Header("Logic Objects")] [SerializeField]
        private TalkAction talkAction;

        [SerializeField] private PlayerInput playerInput;

        private void Awake()
        {
            playerInput.SwitchCurrentActionMap("Default");
        }

        public void ChangeInputMapTo(string map)
        {
            playerInput.SwitchCurrentActionMap(map);
        }
        
        
        private void OnClick()
        {
            Debug.Log("Click");
            talkAction.HandleMouseClick();
        }
    }
}