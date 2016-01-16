using UnityEngine;
using System.Collections;
using Colony.Resources;
using Colony.UI;

namespace Colony.Events {

public class SkunkEvent : Event {

	public SkunkEvent() : base() {
		Text = "A <b>skunk</b> is going to attack our hive and <i>steal your resources</i>!" +
			"\r\nIt'll probably <i>kill some bees</i> too.";

		// Check if player has enough bees to sacrifice
		if (EntityManager.Instance.Bees.Count >= spawner.SkunkSacrificedBees) {
			Text +=	"\r\nYou may <b>sacrifice " + spawner.SkunkSacrificedBees + " bees</b> to stop it immediately." +
				"\r\n\r\nSacrifice bees?";
			Style = EventManager.PopupStyle.YesNo;
		} else {
			Style = EventManager.PopupStyle.Ok;
		}

		Timeout = GameObject.FindObjectOfType<EventSpawner>().SkunkTimeout;
		Level = 2;
	}

	public override void Yes() {
		foreach (var bee in EntityManager.Instance.GetRandomBees(spawner.SkunkSacrificedBees))
			EntityManager.Instance.DestroyBee(bee);
		TextController.Instance.Add("You sacrificed " + spawner.SkunkSacrificedBees + " bees to scare away the skunk.");
		Canceled = true;
		base.Yes();
	}

	public override string Happen() {
		int nBees = Random.Range(spawner.SkunkMinBeesKilled, spawner.SkunkMaxBeesKilled);

		// Shuffle the list of bees to kill random ones
		foreach (GameObject bee in EntityManager.Instance.GetRandomBees(nBees))
			EntityManager.Instance.DestroyBee(bee);

		// Steal honey
		int honeyStolen = Random.Range(spawner.SkunkMinHoneyStolen, spawner.SkunkMaxHoneyStolen);
		resourceManager.RemoveResource(ResourceType.Honey, honeyStolen);

		return "The bear killed <b>" + nBees.ToString() + "</b> of your bees " +
			"and stole <b>" + honeyStolen + "</b> of your honey!";
	}
}
}
