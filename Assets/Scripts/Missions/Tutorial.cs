using Colony.Missions.SimpleMissions;
using System.Collections.Generic;
using UnityEngine;

namespace Colony.Missions
{
    public class Tutorial : MonoBehaviour
    {
        private Queue<Mission> tutorialMissions;

	private GameObject wasps;

        void Awake()
        {
            tutorialMissions = new Queue<Mission>();

            wasps = GameObject.Find("Wasps");
            wasps.SetActive(false);
//		Invoke("eventuallyActivateWasps", 240f);


            //Very, very bad hardcoding
		tutorialMissions.Enqueue(new ClickOn("Click on a Worker Bee", "Left-click on a bee to select it (or drag to select a group of bees).", "WorkerBee"));
		tutorialMissions.Enqueue(new HarvestMission("Harvest a flower", "With a selected bee, right-click on a flower to harvest it"));
		tutorialMissions.Enqueue(new ClickOn("Click on a Queen Bee", "Left-click on a Queen Bee to select it", "QueenBee"));
		tutorialMissions.Enqueue(new BreedMission("Lay an egg", "With a selected Queen Bee, right-click on a hive cell to lay an egg"));
		tutorialMissions.Enqueue(new GrowSpecificBees("Grow 2 Worker Bees", "Left-click on a larva to select it, then right-click on \"Worker Bee\" button in the new menu opened", "WorkerBee", 2));
		tutorialMissions.Enqueue(new RefineMission("Refine some Honey",
			"Left-click on a free hive cell to select it, then left-click on \"Refine Honey\" button in the new menu opened", 
			Cell.RefinedResource.Honey));
            tutorialMissions.Enqueue(new ReachBeesNumber("Reach 10 bees", "", 10));
            tutorialMissions.Enqueue(new ActivateObject(wasps));
		tutorialMissions.Enqueue(new KillEnemies("Kill a wasp", "Left-click and hold to select a group of Worker Bees, then left-click on a wasp to attack it ", "Wasp", 1));
		tutorialMissions.Enqueue(new SpecializeMission("Specialize a Worker Bee into a Forager.", "Left-click on Worker Bee to select it, then right-click on \"Forager\" button in the new menu opened", Specializations.SpecializationType.Forager, 1));
		tutorialMissions.Enqueue(new SpecializeMission("Specialize a Worker Bee into a Guard.", "Left-click on Worker Bee to select it, then right-click on \"Guard\" button in the new menu opened", Specializations.SpecializationType.Guard, 1));
		tutorialMissions.Enqueue(new SpecializeMission("Specialize a Worker Bee into an Inkeeper.", "Left-click on Worker Bee to select it, then right-click on \"Inkeeper\" button in the new menu opened", Specializations.SpecializationType.Inkeeper, 1));
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

	private void eventuallyActivateWasps() {
		if (!wasps.activeSelf)
			wasps.SetActive(true);
	}
    }
}
