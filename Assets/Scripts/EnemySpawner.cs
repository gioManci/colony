using UnityEngine;

namespace Colony
{
    public class EnemySpawner : MonoBehaviour
    {
        public float startSpawning;
        public float spawnInterval;
        public float minSpawnRadius;
        public float maxSpawnRadius;

        void Start()
        {
            Invoke("SpawnWasp", startSpawning);
        }

        private void SpawnWasp()
        {
            float r = Random.Range(minSpawnRadius, maxSpawnRadius);
            float a = Random.Range(0, Mathf.PI * 2);
            Vector2 position = new Vector2(r * Mathf.Cos(a), r * Mathf.Sin(a));
            EntityManager.Instance.CreateWasp(position);
            Invoke("SpawnWasp", spawnInterval);
        }
    }
}
