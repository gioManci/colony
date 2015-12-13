using UnityEngine;
using System.Collections;

namespace Colony.Resources
{
    public class HiveWarehouse : MonoBehaviour
    {
        private ResourceManager resourceManager;

        public bool IsEmpty { get { return resourceManager.IsEmpty; } }

        void Start()
        {
            resourceManager = FindObjectOfType<ResourceManager>();
        }

        public void AddResources(ResourceSet resources)
        {
            resourceManager.AddResources(resources);
        }

        public void RemoveResources(ResourceSet resources)
        {
            resourceManager.RemoveResources(resources);
        }
    }
}