using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Colony {

public class Boundaries : MonoBehaviour {

	public float WorldSize;

	// Use this for initialization
	void Start() {
		transform.localScale = new Vector3(WorldSize, WorldSize, 1f);
		var edge = gameObject.AddComponent<EdgeCollider2D>();
		edge.points = new Vector2[] {
			new Vector2(-0.5f, -0.5f),
			new Vector2(-0.5f, 0.5f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.5f, -0.5f),
			new Vector2(-0.5f, -0.5f)
		};
	}
}

}
