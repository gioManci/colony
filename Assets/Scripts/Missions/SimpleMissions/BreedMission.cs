using Colony.Tasks.BasicTasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Colony.Missions.SimpleMissions
{
    public class BreedMission : Mission
    {
        public BreedMission(string title, string description) :
            base(title, description)
        {

        }

        public override void OnActivate()
        {
            LayEgg.EggLaid += OnEggLaid;
        }

        private void OnEggLaid(GameObject breeder, GameObject larva)
        {
            LayEgg.EggLaid -= OnEggLaid;
            NotifyCompletion(this);
        }

        public override void Dispose()
        {
            LayEgg.EggLaid -= OnEggLaid;
        }

    }
}
