using UnityEngine;

namespace Timeline
{
    public class TalkAction : MonoBehaviour, IAction
    {
        [Header("Timeline")] [SerializeField] private TimelineParser timelineParser;

        public void Handle(string[] arguments)
        {
            throw new System.NotImplementedException();
        }
    }
}