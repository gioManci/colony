using UnityEngine;
using System.Collections.Generic;
using Colony;
using Colony.Resources;

namespace Colony.Events {

class BearEvent : Event {
	public BearEvent(float timeout) : base() {
		Text = "A <b>bear</b> is approaching your beehive to <i>steal your honey</i>!" +
			"\r\nHe will also <i>kill some of your bees</i>.";
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

		// Steal honey
		int honeyStolen = Random.Range(50, 500);
		resourceManager.RemoveResource(ResourceType.Honey, honeyStolen);

		return "The bear killed <b>" + nBees.ToString() + "</b> of your bees " +
			"and stole <b>" + honeyStolen + "</b> of your honey!";
	}
}

}
