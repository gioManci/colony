using UnityEngine;
using System.Collections;

namespace Colony
{
    public class Stats : MonoBehaviour
    {
        public float speed;
        public float visualRadius;
        public float damage;
        public float lifePoints;
        public float loadSize;
        public float loadTime;

        public float Speed { get; private set; }
        public float VisualRadius { get; private set; }
        public float Damage { get; private set; }
        public float LifePoints { get; private set; }
        public float LoadSize { get; private set; }
        public float LoadTime { get; private set; }

        void Start()
        {
            Speed = speed;
            VisualRadius = visualRadius;
            Damage = damage;
            LifePoints = lifePoints;
            LoadSize = loadSize;
            LoadTime = loadTime;
        }
    }
}