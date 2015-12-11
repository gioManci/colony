using UnityEngine;
using System.Collections;

namespace Colony
{
    public class Life : MonoBehaviour
    {
        public int initialLife;
        private int life;

        void Awake()
        {
            life = initialLife;
        }

        public void Decrease(int damage)
        {
            life -= damage;
            if (life < 0)
            {
                EntityManager.Instance.DestroyEntity(gameObject);
            }
        }
    }
}