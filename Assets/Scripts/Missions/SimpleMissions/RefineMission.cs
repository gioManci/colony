﻿using UnityEngine;
using System.Collections;
using Colony.Missions;
using System;

namespace Colony.Missions.SimpleMissions {

public class RefineMission : Mission {

	Cell.RefinedResource refined;

	public RefineMission(string title, string description, Cell.RefinedResource refined) : base(title, description) {
		this.refined = refined;
	}

        public override void Dispose()
        {
            Cell.OnStateChange -= checkCellRefining;
        }

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