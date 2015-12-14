using UnityEngine;

namespace Colony.Events {

public abstract class Event {
	public string Text { get; protected set; }
	public float Timeout { get; protected set; }
	
	public delegate string EventCallback();
	public EventCallback Happen;

	public bool Tick() {
		Timeout -= Time.deltaTime;
		return Timeout <= 0;
	}
}

}
