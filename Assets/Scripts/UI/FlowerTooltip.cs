using UnityEngine;
using UnityEngine.EventSystems;
using Colony.Input;
using System.Collections;

namespace Colony.UI {

public class FlowerTooltip : Tooltip {

	void Start() {
		base.initialize();
	}

	public override void OnPointerEnter(PointerEventData data) {
		foreach (var obj in MouseActions.Instance.GetSelected<Controllable>()) {
			if (obj.gameObject.tag == "WorkerBee") {
				base.show(Camera.main.WorldToScreenPoint(transform.position));
				break;
			}
		}
	}
}

}