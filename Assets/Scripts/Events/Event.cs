using UnityEngine;
using Colony.Resources;
using System.Collections.Generic;

namespace Colony.Events {

public abstract class Event {
	public string Text { get; protected set; }
	public float Timeout { get; protected set; }
	// The seriousness of the Event
	public int Level { get; protected set; }
	public EventManager.PopupStyle Style { get; protected set; }
	public bool IsImmediate { get; protected set; }
	// Used to cancel an event in case of Yes/No choice during LaunchEvent
	public bool Canceled { get; protected set; }

	protected ResourceManager resourceManager;
	protected EventSpawner spawner;

	protected Event() {
		resourceManager = GameObject.FindObjectOfType<ResourceManager>();
		spawner = GameObject.FindObjectOfType<EventSpawner>();
		Style = EventManager.PopupStyle.Ok;
	}

	public abstract string Happen();

	public bool Tick() {
		Timeout -= Time.deltaTime;
		return Timeout <= 0;
	}

	public virtual void Yes() {
		EventManager.Instance.HidePopup();
	}

	public virtual void No() {
		EventManager.Instance.HidePopup();
	}
}

}
