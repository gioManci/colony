using UnityEngine;

namespace Colony
{
    public class EnemySpawner : MonoBehaviour
    {
        public float StartSpawning;
        public float SpawnInterval;
        public float MinSpawnRadius;
        public float MaxSpawnRadius;

        void Start()
        {
            Invoke("spawnWaspsLoop", StartSpawning);
        }

	public GameObject SpawnWasp(float minRadius = -1, float maxRadius = -1)
        {
            float r = Random.Range(minRadius >= 0 ? minRadius : MinSpawnRadius, 
			maxRadius >= 0 ? maxRadius : MaxSpawnRadius);
            float a = Random.Range(0, Mathf.PI * 2);
            Vector2 position = new Vector2(r * Mathf.Cos(a), r * Mathf.Sin(a));
            return EntityManager.Instance.CreateWasp(position);
        }

	private void spawnWaspsLoop() {
		SpawnWasp();
		Invoke("spawnWaspsLoop", SpawnInterval);
	}
    }
}
