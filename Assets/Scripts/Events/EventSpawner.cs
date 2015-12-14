using UnityEngine;

namespace Colony.Events {

public class EventSpawner : MonoBehaviour {
	
	private float timeout = 5;

	void Update() {
		timeout -= Time.deltaTime;
		if (timeout <= 0) {
			timeout = 100000;
			EventManager.Instance.LaunchEvent(new BearEvent());
		}
	}
}

}
