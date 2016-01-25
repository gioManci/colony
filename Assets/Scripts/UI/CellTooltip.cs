using UnityEngine;
using UnityEngine.EventSystems;
using Colony.Input;
using System.Collections;

namespace Colony.UI {

[RequireComponent(typeof(Cell))]
public class CellTooltip : Tooltip {

	private Cell cell;

	void Start() {
		base.initialize();
		cell = GetComponent<Cell>();
	}

	public override void OnPointerEnter(PointerEventData data) {
		foreach (var obj in MouseActions.Instance.GetSelected<Controllable>()) {
			if (obj.gameObject.tag == "QueenBee") {
				if (cell.CellState == Cell.State.Storage) {
					base.show(Camera.main.WorldToScreenPoint(transform.position));
				}
				break;
			}
		}
	}
}

}