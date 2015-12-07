using UnityEngine;
using System.Collections;

namespace Colony.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        private ResourceSet resourcesStock;

        void Awake()
        {
            resourcesStock = new ResourceSet();
        }

        public void AddResources(ResourceSet resources)
        {
            resourcesStock += resources;
        }

        public void AddResource(ResourceType resourceType, int amount)
        {
            if (amount > 0)
            {
                resourcesStock[resourceType] += amount;
            }
        }

        public bool RemoveResource(ResourceType resourceType, int amount)
        {
            if (amount > 0 && resourcesStock[resourceType] >= amount)
            {
                resourcesStock[resourceType] -= amount;
                return true;
            }
            return false;
        }
    }
}