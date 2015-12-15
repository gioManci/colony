using UnityEngine;
using System;
using System.Collections;

namespace Colony.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        public int initialBeeswax;
        public int initialHoney;
        public int initialNectar;
        public int initialPollen;
        public int initialRoyalJelly;
        public int initialWater;

        private ResourceSet resourcesStock;
	public event Action OnResourceChange;

        public bool IsEmpty { get { return resourcesStock.IsEmpty(); } }

        void Awake()
        {
            resourcesStock = new ResourceSet()
                .With(ResourceType.Beeswax, initialBeeswax)
                .With(ResourceType.Honey, initialHoney)
                .With(ResourceType.Nectar, initialNectar)
                .With(ResourceType.Pollen, initialPollen)
                .With(ResourceType.RoyalJelly, initialRoyalJelly)
                .With(ResourceType.Water, initialWater);
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

        public void RemoveResources(ResourceSet resources)
        {
            resourcesStock -= resources;
            if (OnResourceChange != null)
            {
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

	public bool RequireResources(ResourceSet res) {
		for (int i = 0; i < Enum.GetValues(typeof(ResourceType)).Length; ++i) {
			if (resourcesStock[(ResourceType)i] < res[(ResourceType)i])
            {
                return false;
            }
		}
		return true;
	}
    }
}
