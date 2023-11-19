using System;
using Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        [Header("Logic Objects")] [SerializeField]
        private TalkAction talkAction;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.SwitchCurrentActionMap("Default");
        }

        public void ChangeInputMapTo(string map)
        {
            _playerInput.SwitchCurrentActionMap(map);
        }
        
        
        private void OnClick()
        {
            talkAction.HandleMouseClick();
        }
    }
}