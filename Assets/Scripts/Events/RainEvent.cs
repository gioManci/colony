using UnityEngine;
using System.Collections;
using Colony.Resources;

namespace Colony.Events {

public class RainEvent : Event {

	// Use this for initialization
	void Start() {
		IsImmediate = true;
		Image = spawner.RainSprite;
		Level = 1;
	}

	public override string Happen() {
		int n = Random.Range(spawner.RainMinFlowersInvolved, spawner.RainMaxFlowersInvolved);

		// Shuffle the list of bees to kill random ones
		foreach (GameObject obj in EntityManager.Instance.GetRandomResources(n)) {
			var yielder = obj.GetComponent<ResourceYielder>();
			int removedResource = (int)Random.Range(spawner.RainMinPercPollenDecreased,
				spawner.RainMaxPercPollenDecreased) * yielder.initialPollen;
			yielder.Yield(new ResourceSet()
				.With(ResourceType.Pollen, -removedResource)
				.With(ResourceType.Water, (int)(removedResource / 3f)));
		}

		return "It started to <b>rain</b>!" +
			"\r\n" + n + " flowers had their pollen diminished and water increased!";
	}
}

}