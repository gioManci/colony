﻿using System;
using UnityEngine;

namespace Colony.Missions
{
    public abstract class Mission
    {
        private string title;
        private string description;

        public string Title { get { return title; } }

        public string Description { get { return description; } }

        public event Action<Mission> MissionComplete;

        public Mission(string title, string description)
        {
            this.title = title;
            this.description = description;
        }

        public abstract void OnActivate();

        public abstract void OnAccomplished();

        protected void NotifyCompletion(Mission completedMission)
        {
            if (MissionComplete != null)
            {
                MissionComplete(completedMission);
            }
        }
    }
}