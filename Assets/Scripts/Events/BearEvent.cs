using UnityEngine;
using System.Collections.Generic;
using Colony;
using Colony.Resources;

namespace Colony.Events {

class BearEvent : Event {

	public BearEvent() : base() {
		Text = "A <b>bear</b> is approaching your beehive to <i>steal your honey</i>!" +
			"\r\nIt will also <i>kill some of your bees</i>.";

		Timeout = spawner.BearTimeout;
		Image = spawner.BearImage;
		Level = 3;
	}

	public override Event Init() {
		// TODO: check if player has enough guard bees
		if (true) {
			Text +=	"\r\nYou may <b>sacrifice " + spawner.BearSacrificedBees + " guard bees</b> to stop it immediately." +
				"\r\n\r\nSacrifice guard bees?";
			Style = EventManager.PopupStyle.YesNo;
		} else {
			Style = EventManager.PopupStyle.Ok;
		}
	
		return this;
	}

	public override void Yes() {
		Debug.Log("sacrificed bees.");
		Canceled = true;
		base.Yes();
	}

	public override string Happen() {
		// FIXME: when we have specializations, this will
		// depend on number of warrior bees in the hive;
		// for now, just kill a random number of bees.
		int nBees = Random.Range(spawner.BearMinBeesKilled, spawner.BearMaxBeesKilled);

		// Shuffle the list of bees to kill random ones
		foreach (GameObject bee in EntityManager.Instance.GetRandomBees(nBees))
			EntityManager.Instance.DestroyBee(bee);

		// Steal honey
		int honeyStolen = Random.Range(spawner.BearMinHoneyStolen, spawner.BearMaxHoneyStolen);
		resourceManager.RemoveResource(ResourceType.Honey, honeyStolen);

		return "The bear killed <b>" + nBees.ToString() + "</b> of your bees " +
			"and stole <b>" + honeyStolen + "</b> of your honey!";
	}
}

}
