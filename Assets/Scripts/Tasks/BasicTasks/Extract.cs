using System;
using UnityEngine;
using Colony.Resources;

namespace Colony.Tasks.BasicTasks
{
    public class Extract : Task
    {
        private GameObject resource;
        private float timeFromLastExtraction;
        private Stats stats;
        private ResourceYielder yielder;
        private BeeLoad load;

        public Extract(GameObject agent, GameObject resource) : base(agent, TaskType.Extract)
        {
            this.resource = resource;
            timeFromLastExtraction = 0.0f;
            stats = agent.GetComponent<Stats>();
            yielder = resource.GetComponent<ResourceYielder>();
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

            timeFromLastExtraction += Time.deltaTime;

            if (load.IsFull)
            {
                status = Status.Completed;
            }
            else if (yielder.IsDepleted)
            {
                status = Status.Failed;
            }
            else if (timeFromLastExtraction > stats.LoadTime)
            {
                ResourceSet requestAmount = new ResourceSet()
                    .With(ResourceType.Nectar, yielder.defaultNectarYield)
                    .With(ResourceType.Pollen, yielder.defaultPollenYield)
                    .With(ResourceType.Water, yielder.defaultWaterYield)
                    .With(ResourceType.Beeswax, yielder.defaultBeeswaxYield)
                    .With(ResourceType.Honey, yielder.defaultHoneyYield)
                    .With(ResourceType.RoyalJelly, yielder.defaultRoyalJellyYield);

                ResourceSet result = yielder.Yield(requestAmount);

                if (result.IsEmpty())
                {
                    return Status.Failed;
                }

                load.AddResources(result);

                timeFromLastExtraction = 0.0f;
            }

            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
