using UnityEngine;

namespace Colony {
public class EnemySpawner : MonoBehaviour {
	public float StartSpawning = 120;
	public float SpawnInterval = 30;
	public float MinSpawnRadius = 10;
	public float MaxSpawnRadius = 49;
	public float StartSpawningHornets = 240;
	public float WaspsToHornetRatio = 20;

	private bool canSpawnHornets;

	void Start() {
		Invoke("setHornetFlagOn", StartSpawningHornets);
		Invoke("spawnWaspsLoop", StartSpawning);
	}

	public GameObject SpawnWasp(float minRadius = -1, float maxRadius = -1) {
		float r = Random.Range(minRadius >= 0 ? minRadius : MinSpawnRadius, 
			                    maxRadius >= 0 ? maxRadius : MaxSpawnRadius);
		float a = Random.Range(0, Mathf.PI * 2);
		Vector2 position = new Vector2(r * Mathf.Cos(a), r * Mathf.Sin(a));

		float rand = Random.Range(0f, WaspsToHornetRatio);

		return (!canSpawnHornets || (int)rand > 0)
			? EntityManager.Instance.CreateWasp(position)
			: EntityManager.Instance.CreateHornet(position);
	}

	private void spawnWaspsLoop() {
		SpawnWasp();
		Invoke("spawnWaspsLoop", SpawnInterval);
	}

	private void setHornetFlagOn() {
		canSpawnHornets = true;
	}
}
}
