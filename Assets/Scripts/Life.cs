using UnityEngine;
using System.Collections;

namespace Colony
{
    public class Life : MonoBehaviour
    {
        public int initialLife;
        public ParticleSystem onHit;

        public int CurrentLife { get; private set; }

        void Awake()
        {
            CurrentLife = initialLife;
        }

        public void Decrease(int damage)
        {
            CurrentLife -= damage;
            if (onHit != null)
            {
                onHit.Play();
            }
            if (CurrentLife < 0)
            {
                EntityManager.Instance.DestroyEntity(gameObject);
            }
        }
    }
}