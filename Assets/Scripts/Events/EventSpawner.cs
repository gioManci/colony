using UnityEngine;

namespace Colony.Events {

public class EventSpawner : MonoBehaviour {
	
	public float eventTimeout = 5;
    public float bearTimeout;

	void Update() {
		eventTimeout -= Time.deltaTime;
		if (eventTimeout <= 0) {
			eventTimeout = 100000;
			EventManager.Instance.LaunchEvent(new BearEvent(bearTimeout));
		}
	}
}

}
