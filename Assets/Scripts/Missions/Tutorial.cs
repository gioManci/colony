﻿using Colony.Missions.SimpleMissions;
using System.Collections.Generic;
using UnityEngine;

namespace Colony.Missions
{
    public class Tutorial : MonoBehaviour
    {
        private Queue<Mission> tutorialMissions;

        void Awake()
        {
            tutorialMissions = new Queue<Mission>();

            //Very, very bad hardcoding
            tutorialMissions.Enqueue(new ClickOn("Click on a Worker Bee", "Left-click on a bee to select it.", "WorkerBee"));
            tutorialMissions.Enqueue(new HarvestMission("Harvest a flower", ""));
            tutorialMissions.Enqueue(new ClickOn("Click on a Queen Bee", "", "QueenBee"));
            tutorialMissions.Enqueue(new BreedMission("Lay an egg", ""));
            tutorialMissions.Enqueue(new GrowSpecificBees("Grow 2 Worker Bees", "", "WorkerBee", 2));
            tutorialMissions.Enqueue(new GrowSpecificBees("Grow a Drone Bee", "", "DroneBee", 1));
            //Refine resources
            tutorialMissions.Enqueue(new ReachBeesNumber("Reach 20 bees", "", 20));
            tutorialMissions.Enqueue(new KillEnemies("Kill a wasp", "", "Wasp", 1));
            tutorialMissions.Enqueue(new SpecializeMission("Specialize a Worker Bee into a Forager.", "", Specializations.SpecializationType.Forager, 1));
            tutorialMissions.Enqueue(new SpecializeMission("Specialize a Worker Bee into a Guard.", "", Specializations.SpecializationType.Guard, 1));
            tutorialMissions.Enqueue(new SpecializeMission("Specialize a Worker Bee into an Inkeeper.", "", Specializations.SpecializationType.Inkeeper, 1));
            //Survive an event
            //(create a new hive)

            foreach (Mission mission in tutorialMissions)
            {
                mission.MissionComplete += OnMissionComplete;
            }

            Invoke("StartTutorial", 1.0f);
        }

        public void StartTutorial()
        {
            MissionManager.Instance.AddMission(tutorialMissions.Peek());
        }

        private void OnMissionComplete(Mission completedMission)
        {
            if (completedMission == tutorialMissions.Peek())
            {
                tutorialMissions.Dequeue().MissionComplete -= OnMissionComplete;
            }
            if (tutorialMissions.Count > 0)
            {
                MissionManager.Instance.AddMission(tutorialMissions.Peek());
            }
        }
    }
}