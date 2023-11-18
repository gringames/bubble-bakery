using Timeline;
using UnityEngine;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        [Header("Logic Objects")] [SerializeField]
        private TalkAction talkAction;


        private void OnClick()
        {
            talkAction.HandleMouseClick();
        }
    }
}