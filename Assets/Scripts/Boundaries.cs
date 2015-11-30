using UnityEngine;
using System.Collections;

public class Boundaries : MonoBehaviour {

	public float worldSize;

	// Use this for initialization
	void Start() {
		transform.localScale = new Vector3(worldSize, worldSize, 1f);
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
