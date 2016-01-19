using UnityEngine;
using System.Collections;
using Colony.Missions;

namespace Colony.Missions.SimpleMissions {

public class RefineMission : Mission {

	Cell.RefinedResource refined;

	public RefineMission(string title, string description, Cell.RefinedResource refined) : base(title, description) {}
	
	public override void OnActivate() {
		Cell.OnStateChange += checkCellRefining;
	}

	private void checkCellRefining(GameObject cell) {
		var c = cell.GetComponent<Cell>();
		if (c.CellState == Cell.State.Refine && c.Refined == refined) {
			Cell.OnStateChange -= checkCellRefining;
			NotifyCompletion(this);
		}
	}
}

}