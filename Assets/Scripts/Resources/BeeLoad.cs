using UnityEngine;
using System.Collections;

namespace Colony.Resources
{
    public class BeeLoad : MonoBehaviour
    {
        private Stats stats;

        public ResourceSet Load { get; private set; }

        public bool IsFull { get { return Load.GetSize() >= stats.LoadSize; } }

        void Start()
        {
            Load = new ResourceSet();
            stats = gameObject.GetComponent<Stats>();
        }

        public void AddResources(ResourceSet amount)
        {
            Load += amount;
        }

        public void Clear()
        {
            Load.Clear();
        }
    }
}