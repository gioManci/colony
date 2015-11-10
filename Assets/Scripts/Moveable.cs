using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {
	const float TargetDistanceDelta = 0.5f;
	const float SpeedIncrement = 0.3f;

	// These are set via Unity, unit-wise
	public float speedCap;

	private Vector2 target;
	private float speed; // current speed
	private bool moving = false;
	private Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void Move(Vector2 where) {
		stop();
		Debug.Log(gameObject + " moving to " + where);
		target = where;
		speed = 0f;
		moving = true;
	}

	void FixedUpdate() {
		if (moving) {
			// Accelerate till speedCap
			if (rb.velocity.magnitude < speedCap)
				rb.AddForce(target - (Vector2)transform.position);
			// TODO: rotate
			// Stop when target is approached
			if (Vector2.Distance(target, (Vector2)transform.position) < TargetDistanceDelta)
				stop();
		}
	}

	private void stop() {
		rb.velocity = new Vector2(0, 0);
		moving = false;
	}
}
