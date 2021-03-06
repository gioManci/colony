﻿using UnityEngine;
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

        public void OnHit(GameObject enemy)
        {
            Stats enemyStats = enemy.GetComponent<Stats>();
            CurrentLife -= enemyStats.Damage;
            if (onHit != null)
            {
                onHit.gameObject.SetActive(false);
                onHit.gameObject.SetActive(true);
            }
            if (CurrentLife <= 0)
            {
                EntityManager.Instance.DestroyEntity(gameObject);
            }
        }
    }
}