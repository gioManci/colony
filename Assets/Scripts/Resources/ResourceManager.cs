using UnityEngine;
using System;
using System.Collections;

namespace Colony.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        private ResourceSet resourcesStock;
	public event Action OnResourceChange;

        void Awake()
        {
            resourcesStock = new ResourceSet();
        }

        public void AddResources(ResourceSet resources)
        {
            resourcesStock += resources;
	    if (OnResourceChange != null)
		    OnResourceChange();
        }

        public void AddResource(ResourceType resourceType, int amount)
        {
            if (amount > 0)
            {
                resourcesStock[resourceType] += amount;
	        if (OnResourceChange != null)
		    OnResourceChange();
            }
        }

        public bool RemoveResource(ResourceType resourceType, int amount)
        {
            if (amount > 0 && resourcesStock[resourceType] >= amount)
            {
                resourcesStock[resourceType] -= amount;
	        if (OnResourceChange != null)
		    OnResourceChange();
                return true;
            }
            return false;
        }

	public int GetResource(ResourceType type) {
		return resourcesStock[type];
	}
    }
}
