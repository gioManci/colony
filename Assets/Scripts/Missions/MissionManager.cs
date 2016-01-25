using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Colony.Missions
{
    public class MissionManager : MonoBehaviour
    {
        private List<Mission> missions;

        private GameObject missionPanel;

        public Mission CurrentMission { get; private set; }
        public bool HasActiveMission { get { return CurrentMission != null; } }

        public static MissionManager Instance { get; private set; }

        private MissionManager()
        {
            missions = new List<Mission>();
            CurrentMission = null;
        }

        void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Transform canvasTransform = GameObject.Find("Canvas").transform;
            for (int i=0;i<canvasTransform.childCount;i++)
            {
                GameObject child = canvasTransform.GetChild(i).gameObject;
                if (child.name == "MissionPanel")
                {
                    missionPanel = child;
                }
            }
        }

        public void AddMission(Mission mission)
        {
            missions.Add(mission);
            mission.MissionComplete += OnMissionComplete;
            if (!HasActiveMission)
            {
                ActivateMission(mission);
            }
        }

        private void OnMissionComplete(Mission mission)
        {
            mission.OnAccomplished();
            mission.MissionComplete -= OnMissionComplete;
            missions.Remove(mission);
            if (missions.Count > 0)
            {
                ActivateMission(missions[0]);
            }
            else
            {
                CurrentMission = null;
                if (missionPanel != null)
               	    missionPanel.SetActive(false);
            }
        }

        private void ActivateMission(Mission mission)
        {
            CurrentMission = mission;
            mission.OnActivate();
	
            // NOTE: if mission.Title is "", it's a "fake mission", i.e. some event
            // which needs to be triggered during the tutorial (e.g. ActivateObject)
            if (mission.Title.Length > 0) {
                if (!missionPanel.activeSelf)
                    missionPanel.SetActive(true);
  
                missionPanel.SendMessage("SetTitle", mission.Title);
                missionPanel.SendMessage("SetDescription", mission.Description);
            }
        }

        void OnDestroy()
        {
            foreach (Mission mission in missions)
            {
                mission.Dispose();
            }
        }
    }
}