using UnityEngine;
using Colony.Resources;

namespace Colony.Events {

public abstract class Event {
	public string Text { get; protected set; }
	public float Timeout { get; protected set; }
	
	public delegate string EventCallback();
	public EventCallback Happen;

	protected ResourceManager resourceManager;

	protected Event() {
		resourceManager = GameObject.FindObjectOfType<ResourceManager>();
	}

	public bool Tick() {
		Timeout -= Time.deltaTime;
		return Timeout <= 0;
	}
}

}
