using UnityEngine;
using System.Collections;

namespace Colony.Resources
{
    public class HiveWarehouse : MonoBehaviour
    {
        private ResourceManager resourceManager;

        void Start()
        {
            resourceManager = FindObjectOfType<ResourceManager>();
        }

        public void AddResources(ResourceSet resources)
        {
            resourceManager.AddResources(resources);
        }
    }
}