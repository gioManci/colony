using UnityEngine;
using System.Collections;

namespace Colony
{
    public class Stats : MonoBehaviour
    {
        public float speed;
        public float visualRadius;
        public int damage;
        public float loadSize;
        public float loadTime;
        public float reactionTime;

        public float Speed { get; private set; }
        public float VisualRadius { get; private set; }
        public int Damage { get; private set; }
        public float LoadSize { get; private set; }
        public float LoadTime { get; private set; }
        public float ReactionTime { get; private set; }

        void Start()
        {
            Speed = speed;
            VisualRadius = visualRadius;
            Damage = damage;
            LoadSize = loadSize;
            LoadTime = loadTime;
            ReactionTime = reactionTime;
        }
    }
}