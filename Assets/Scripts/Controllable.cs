using UnityEngine;
using System.Collections;
using Colony.Input;

namespace Colony {
	
public class Controllable : MonoBehaviour {

	// Use this for initialization
	void Start() {
		MouseActions.Instance.AddControllable(this);
	}
	
	void OnDestroy() {
		MouseActions.Instance.RemoveControllable(this);
	}
}

}