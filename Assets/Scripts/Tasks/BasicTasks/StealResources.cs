using Colony.Resources;
using System;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class StealResources : Task
    {
        private HiveWarehouse warehouse;
        private Stats stats;
        private float timeFromLastTheft = 0.0f;

        public StealResources(GameObject agent, HiveWarehouse hiveWarehouse) : base(agent, TaskType.StealResources)
        {
            warehouse = hiveWarehouse;
            stats = agent.GetComponent<Stats>();
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
            timeFromLastTheft += Time.deltaTime;

            if (warehouse.IsEmpty)
            {
                status = Status.Completed;
            }
            else if (timeFromLastTheft >= stats.LoadTime)
            {
                ResourceSet toSteal = new ResourceSet()
                    .With(ResourceType.Beeswax, 10)
                    .With(ResourceType.Honey, 10)
                    .With(ResourceType.Nectar, 10)
                    .With(ResourceType.Pollen, 10)
                    .With(ResourceType.RoyalJelly, 10)
                    .With(ResourceType.Water, 10);
                warehouse.RemoveResources(toSteal);

                timeFromLastTheft = 0.0f;
            }

            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
