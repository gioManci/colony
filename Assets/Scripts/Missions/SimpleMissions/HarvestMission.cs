using Colony.Tasks.BasicTasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Colony.Missions.SimpleMissions
{
    public class HarvestMission : Mission
    {
        public HarvestMission(string title, string description) : 
            base(title, description)
        {

        }

        public override void OnActivate()
        {
            Extract.ResourceExtracted += OnResourceExtracted;
        }

        private void OnResourceExtracted(GameObject forager, GameObject resource)
        {
            Extract.ResourceExtracted -= OnResourceExtracted;
            NotifyCompletion(this);
        }
    }
}
