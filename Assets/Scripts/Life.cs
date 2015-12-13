using UnityEngine;
using System.Collections;

namespace Colony
{
    public class Life : MonoBehaviour
    {
        public int initialLife;

        public int CurrentLife { get; private set; }

        void Awake()
        {
            CurrentLife = initialLife;
        }

        public void Decrease(int damage)
        {
            CurrentLife -= damage;
            if (CurrentLife < 0)
            {
                EntityManager.Instance.DestroyEntity(gameObject);
            }
        }
    }
}