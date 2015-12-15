using UnityEngine;
using System;

namespace Colony.Resources
{
    public class ResourceYielder : MonoBehaviour
    {
        public int initialPollen;
        public int initialNectar;
        public int initialWater;
        public int initialHoney;
        public int initialRoyalJelly;
        public int initialBeeswax;
        public int defaultPollenYield;
        public int defaultNectarYield;
        public int defaultWaterYield;
        public int defaultHoneyYield;
        public int defaultRoyalJellyYield;
        public int defaultBeeswaxYield;

        private ResourceSet resources;

        public bool IsDepleted { get { return resources.IsEmpty(); } }

        void Start()
        {
            resources = new ResourceSet()
                .With(ResourceType.Beeswax, initialBeeswax)
                .With(ResourceType.Honey, initialHoney)
                .With(ResourceType.Nectar, initialNectar)
                .With(ResourceType.Pollen, initialPollen)
                .With(ResourceType.RoyalJelly, initialRoyalJelly)
                .With(ResourceType.Water, initialWater);
        }

        public ResourceSet Yield(ResourceSet request)
        {
            ResourceSet result = new ResourceSet();
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                result[type] = Mathf.Min(request[type], resources[type]);
            }
            resources -= request;

            if (resources.IsEmpty())
            {
                gameObject.SetActive(false);
            }

            return result;
        }
    }
}
