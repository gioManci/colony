using Colony.Resources;
using System;
using UnityEngine;
using Colony.UI;

namespace Colony.Tasks.BasicTasks
{
    public class Deposit : Task
    {
        private BeeLoad load;
        private HiveWarehouse targetHive;

        public Deposit(GameObject agent, GameObject targetBeehive) : base(agent, TaskType.Deposit)
        {
            targetHive = targetBeehive.GetComponent<HiveWarehouse>();
            load = agent.GetComponent<BeeLoad>();
        }

        public override void Activate()
        {
            status = Status.Active;
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            targetHive.AddResources(load.Load);
            load.Clear();
            if (agent.GetComponent<Selectable>().IsSelected)
                UIController.Instance.SetBeeLoadText(load);
            status = Status.Completed;

            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
