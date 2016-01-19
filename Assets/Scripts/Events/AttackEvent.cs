using UnityEngine;
using System.Collections;
using Colony.Tasks.BasicTasks;
using Colony;

namespace Colony.Events {

public class AttackEvent : Event {

	private Boundaries bounds;

	public AttackEvent() {
		Text = "A horde of <b>Wasps</b> is going to attack our beehive!";
		Timeout = GameObject.FindObjectOfType<EventSpawner>().AttackTimeout;
		Level = 3;
		Image = spawner.AttackSprite;
		bounds = GameObject.Find("Ground").GetComponent<Boundaries>();
	}

	public override string Happen() {
		int nWasps = Random.Range(spawner.AttackMinWaspsSpawned, spawner.AttackMaxWaspsSpawned);
		var first = GameObject.FindObjectOfType<EnemySpawner>().SpawnWasp(
			0.6f * bounds.WorldSize/2f, 0.9f * bounds.WorldSize/2f);
		var attackedHive = EntityManager.Instance.Beehives[0];
		Debug.Log(attackedHive.transform.position);
		first.GetComponent<FreeWilled>().DoMove(attackedHive.transform.position);
		var pos = first.transform.position;
		// Spawn a horde of wasps nearby
		for (int i = 1; i < nWasps; ++i) {
			float r = Random.Range(0.7f, 5f);
			float a = Random.Range(0f, 2 * Mathf.PI);
			var npos = pos + new Vector3(r * Mathf.Cos(a), r * Mathf.Sin(a), 0);
			var wasp = EntityManager.Instance.CreateWasp(npos);
			wasp.GetComponent<FreeWilled>().DoMove(attackedHive.transform.position);
		}

		return nWasps + " hostile wasps are going to attack our main beehive!";
	}
}

}