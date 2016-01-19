using System;
using UnityEngine;

namespace Colony.Missions.SimpleMissions
{
    public class GrowSpecificBees : Mission
    {
        private int count;
        private int targetAmount;
        private string beeType;

        public GrowSpecificBees(string title, string description, string beeTag, int amount) :
            base(title, description)
        {
            beeType = beeTag;
            count = 0;
            targetAmount = amount;
        }

        public override void OnActivate()
        {
            EntityManager.Instance.BeeCreated += OnBeeCreated;
        }

        private void OnBeeCreated(GameObject bee)
        {
            if (bee.tag == beeType)
            {
                if (++count >= targetAmount)
                {
                    EntityManager.Instance.BeeCreated -= OnBeeCreated;
                    NotifyCompletion(this);
                }
            }
        }
    }
}
