using UnityEngine;
using System.Collections.Generic;

namespace Colony.Hive {

class Hive : MonoBehaviour {

	public uint radius;
	public GameObject cellTemplate;

	public int Id { get; private set; }
	public static int ids = 0;

	private List<Cell> cells = new List<Cell>();

	void Awake() {
		Id = ids++;
	}

	void Start() {
		createCells(radius);		
	}

	private delegate void AlgorithmStep(float i, float r);

	// Creates a hive of radius `r` with cells correctly placed.
	// r = 0 means a single cell.
	private void createCells(uint r) {
		var c = (GameObject)GameObject.Instantiate(cellTemplate,
				transform.position, transform.rotation);
		c.transform.parent = transform;

		if (r == 0) return;
		
		const float celldist = 1.8f;

		AlgorithmStep step = (j, radius) => {
			float angle = j * 2 * Mathf.PI / 6f;
			var pos = transform.position;
			float dx = celldist * Mathf.Sin(angle + 4 * Mathf.PI / 6f),
			      dy = celldist * Mathf.Cos(angle + 4 * Mathf.PI / 6f);


			// Step r times from center
			for (uint i = 0; i < radius; ++i) {
				pos.x += celldist * Mathf.Sin(angle);
				pos.y += celldist * Mathf.Cos(angle);
			}
				
			// Moving with an angle of 120° CW from this direction, create r cells
			for (uint i = 0; i < radius; ++i) {
				pos.x += dx;
				pos.y += dy;
				var cell = (GameObject)GameObject.Instantiate(cellTemplate,
						pos, transform.rotation);
				cell.transform.parent = transform;
			}
		};

		for (int j = 1; j <= r; ++j)
			for (int i = 0; i < 6; ++i)
				step(i, j);
	}
}

}
