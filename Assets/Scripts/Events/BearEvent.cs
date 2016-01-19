using UnityEngine;
using System.Collections.Generic;
using Colony;
using Colony.Resources;
using Colony.Specializations;
using Colony.UI;

namespace Colony.Events {

class BearEvent : Event {

	public BearEvent() : base() {
		Text = "A <b>bear</b> is approaching your beehive to <i>steal your honey</i>!" +
			"\r\nIt will also <i>kill some of your bees</i>.";

		Timeout = spawner.BearTimeout;
		Image = spawner.BearSprite;
		Level = 3;
	}

	public override Event Init() {
		if (EntityManager.Instance.GuardBees.Count >= spawner.BearSacrificedBees) {
			Text +=	"\r\nYou may <b>sacrifice " + spawner.BearSacrificedBees + " guard bees</b> to stop it immediately." +
				"\r\n\r\nSacrifice guard bees?";
			Style = EventManager.PopupStyle.YesNo;
		} else {
			Style = EventManager.PopupStyle.Ok;
		}
	
		return this;
	}

	public override void Yes() {
		foreach (var bee in EntityManager.Instance.GetRandomBees(spawner.BearSacrificedBees, SpecializationType.Guard))
			EntityManager.Instance.DestroyBee(bee);
		TextController.Instance.Add("You sacrificed " + spawner.SkunkSacrificedBees + " bees to scare away the bear.");
		Canceled = true;
		base.Yes();
	}

	public override string Happen() {
		int nBees = Random.Range(spawner.BearMinBeesKilled, spawner.BearMaxBeesKilled);

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
