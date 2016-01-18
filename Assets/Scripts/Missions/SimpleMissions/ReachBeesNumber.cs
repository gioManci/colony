using System;
using UnityEngine;

namespace Colony.Missions.SimpleMissions
{
    public class ReachBeesNumber : Mission
    {
        private int beesNumberToReach;

        public ReachBeesNumber(string title, string description, int beesNumber) :
            base(title, description)
        {
            beesNumberToReach = beesNumber;
        }

        public override void OnAccomplished()
        {
            
        }

        public override void OnActivate()
        {
            if (EntityManager.Instance.Bees.Count >= beesNumberToReach)
            {
                NotifyCompletion(this);
            }
            else
            {
                EntityManager.Instance.WorkerBeeCreated += OnBeeCreated;
                EntityManager.Instance.QueenBeeCreated += OnBeeCreated;
                EntityManager.Instance.DroneBeeCreated += OnBeeCreated;
            }
        }

        private void OnBeeCreated(GameObject bee)
        {
            if (EntityManager.Instance.Bees.Count >= beesNumberToReach)
            {
                EntityManager.Instance.WorkerBeeCreated -= OnBeeCreated;
                EntityManager.Instance.QueenBeeCreated -= OnBeeCreated;
                EntityManager.Instance.DroneBeeCreated -= OnBeeCreated;
                NotifyCompletion(this);
            }
        }
    }
}
