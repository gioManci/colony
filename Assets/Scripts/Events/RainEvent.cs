using UnityEngine;
using System.Collections;
using Colony.Resources;

namespace Colony.Events {

public class RainEvent : Event {

	public int MinFlowersInvolved = 500;
	public int MaxFlowersInvolved = 2000;

	// Use this for initialization
	void Start() {
		IsImmediate = true;
		Level = 1;
	}

	public override string Happen() {
		int n = Random.Range(MinFlowersInvolved, MaxFlowersInvolved);

		// Shuffle the list of bees to kill random ones
		foreach (GameObject obj in EntityManager.Instance.GetRandomResources(n)) {
			var yielder = obj.GetComponent<ResourceYielder>();
			int removedResource = (int)Random.Range(0.2f, 0.9f) * yielder.initialPollen;
			yielder.Yield(new ResourceSet()
				.With(ResourceType.Pollen, -removedResource)
				.With(ResourceType.Water, (int)(removedResource / 3f)));
		}

		return "It started to <b>rain</b>!" +
			"\r\n" + n + " flowers had their pollen diminished and water increased!";
	}
}

}