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
        public GameObject counterPanel;

        private bool noQueen = false;
        private bool noWorker = false;

        void Start()
        {
            EntityManager.Instance.DestroyingQueenBee += OnQueenBeeDead;
            EntityManager.Instance.DestroyingWorkerBee += OnWorkerBeeDead;
            EntityManager.Instance.QueenBeeCreated += OnQueenCreated;
            EntityManager.Instance.WorkerBeeCreated += OnWorkerCreated;
        }

        private void OnQueenCreated(GameObject queen)
        {
            if (noQueen)
            {
                noQueen = false;
            }
        }

        private void OnWorkerCreated(GameObject queen)
        {
            if (noWorker)
            {
                noWorker = false;
            }
        }

        private void OnWorkerBeeDead(GameObject obj)
        {
            bool hasWorkers = EntityManager.Instance.GetBeeCount("WorkerBee") > 0;

            if (!hasWorkers)
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
                        ShowForceCreateScreen("QueenBee", 90.0f);
                    }
                }
            }
        }

        private void ShowEndGameScreen()
        {
            TimeSpan survivalTime = TimeSpan.FromSeconds(Time.time);
            string message = "You survived " + string.Format("{0:00}h:{1:00}m:{2:00}s",
                survivalTime.Hours,
                survivalTime.Minutes,
                survivalTime.Seconds);

            endGameScreen.SetActive(true);
            endGameScreen.SendMessage("SetMessage", message);
        }

        private void ShowForceCreateScreen(string beeTag, float createWithinTime)
        {
            //Warning: Bad coding ahead
            switch (beeTag)
            {
                case "WorkerBee":
                    if (noWorker || noQueen)
                    {
                        return;
                    }
                    else
                    {
                        noWorker = true;
                    }
                    break;
                case "QueenBee":
                    if (noQueen)
                    {
                        return;
                    }
                    else
                    {
                        noQueen = true;
                    }
                    break;
            }
            forceToCreateScreen.SetActive(true);
            switch (beeTag)
            {
                case "WorkerBee":
                    forceToCreateScreen.SendMessage("SetMessage", "Create a worker bee or the colony won't survive!");
                    break;
                case "QueenBee":
                    forceToCreateScreen.SendMessage("SetMessage", "Create a queen bee or the colony won't survive!");
                    break;
            }
            forceToCreateScreen.SendMessage("SetCreateWithinTime", createWithinTime);

            counterPanel.SetActive(true);
            CounterController cc = counterPanel.GetComponent<CounterController>();
            cc.StartTime = createWithinTime;
            cc.TimeExpired += OnTimeExpired;
            cc.StartCountdown();
        }

        private void OnTimeExpired(CounterController cc)
        {
            cc.TimeExpired -= OnTimeExpired;
            cc.Stop();
            counterPanel.SetActive(false);
            ShowEndGameScreen();
        }
    }
}
