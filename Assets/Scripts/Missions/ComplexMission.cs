using System;
using System.Collections;
using UnityEngine;

namespace Colony.Missions
{
    public abstract class ComplexMission : Mission
    {
        public ComplexMission(string title, string description) :
            base(title, description)
        {
        }

        public void AddSubMission(Mission mission)
        {
            //some stuff...
            mission.MissionComplete += OnSubMissionComplete;
        }

        private void OnSubMissionComplete(Mission mission)
        {
            
        }
    }
}
