using UnityEngine;
using System.Collections.Generic;
using Colony;

namespace Colony.Events {

class BearEvent : Event {
	public BearEvent(float timeout) {
		Text = "A bear is approaching your beehive to steal your honey!\r\nHe will also kill some of your bees.";
		Timeout = timeout;
		Happen = consequences;
	}

	private string consequences() {
		// FIXME: when we have specializations, this will
		// depend on number of warrior bees in the hive;
		// for now, just kill a random number of bees.
		int nBees = Random.Range(2, 8);

		// Shuffle the list of bees to kill random ones
		var listCopy = new List<GameObject>(EntityManager.Instance.Bees);
		listCopy.Sort((x, y) => 1 - 2 * Random.Range(0, 1));
		foreach (GameObject bee in listCopy.GetRange(0, nBees))
			EntityManager.Instance.DestroyBee(bee);

		return "The bear killed " + nBees.ToString() + " of your bees!";
	}
}

}
