using System;
using Colony.Resources;
using Colony.UI;
using UnityEngine;

namespace Colony
{
    public class GameManager : MonoBehaviour
    {
	public const string SceneName = "beta";

        public GameObject endGameScreen;
        public GameObject forceToCreateScreen;

        void Start()
        {
            EntityManager.Instance.DestroyingQueenBee += OnQueenBeeDead;
            EntityManager.Instance.DestroyingWorkerBee += OnWorkerBeeDead;
        }

        private void OnWorkerBeeDead(GameObject obj)
        {
            if (EntityManager.Instance.GetBeeCount("WorkerBee") == 0)
            {
                ShowForceCreateScreen("WorkerBee", 60.0f);
            }
        }

        private void OnQueenBeeDead(GameObject queen)
        {
            bool hasQueen = EntityManager.Instance.GetBeeCount("QueenBee") > 0;
            bool hasLarvae = EntityManager.Instance.Larvae.Count > 0;
            bool hasWorkers = EntityManager.Instance.GetBeeCount("WorkerBee") > 0;

            if (!hasQueen)
            {
                if (!hasLarvae)
                {
                    ShowEndGameScreen();
                }
                else
                {
                    ResourceManager rm = FindObjectOfType<ResourceManager>();
                    if (!rm.RequireResources(Costs.QueenBee))
                    {
                        if (!hasWorkers)
                        {
                            ShowEndGameScreen();
                        }
                        else
                        {
                            ShowForceCreateScreen("QueenBee", 300.0f);
                        }
                    }
                    else
                    {
                        ShowForceCreateScreen("QueenBee", 60.0f);
                    }
                }
            }
        }

        private void ShowEndGameScreen()
        {
            TimeSpan survivalTime = TimeSpan.FromSeconds(Time.time);
            string message = "You outlasted " + string.Format("{0}h:{1}m:{2}s",
                survivalTime.Hours,
                survivalTime.Minutes,
                survivalTime.Seconds);

            endGameScreen.SetActive(true);
            endGameScreen.SendMessage("SetMessage", message);
        }

        private void ShowForceCreateScreen(string beeTag, float createWithinTime)
        {
            forceToCreateScreen.SetActive(true);
            switch (beeTag)
            {
                case "WorkerBee":
                    forceToCreateScreen.SendMessage("SetMessage", "");
                    break;
                case "QueenBee":
                    forceToCreateScreen.SendMessage("SetMessage", "");
                    break;
            }
            forceToCreateScreen.SendMessage("SetCreateWithinTime", createWithinTime);
        }
    }
}
