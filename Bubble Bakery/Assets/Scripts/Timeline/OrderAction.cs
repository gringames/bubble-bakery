﻿using UnityEngine;

namespace Timeline
{
    public class OrderAction : MonoBehaviour,  IAction
    {
        [Header("Timeline")] [SerializeField] private TimelineParser timelineParser;

        public void Handle(string[] arguments)
        {
            throw new System.NotImplementedException();
        }
        
        private void InformTimelineToGoOn()
        {
            timelineParser.ParseNextLine();
        }
    }
}