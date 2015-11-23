using UnityEngine;
using System.Collections;
using Colony.Input;

namespace Colony {

public class Moveable : MonoBehaviour {
	// Distance from target considered 'acceptable' to stop at
	const float TargetDistanceDelta = 0.5f;
	// Acceleration per-frame, in units of speedCap
	const float SpeedIncrement = 0.05f;

	// These are set via Unity, unit-wise
	public float speedCap;

	private Vector2 target, origin, direction;
	private bool moving = false;
	private float speed, lastSpeed;

	void Start() {
		MouseActions.Instance.AddMoveable(this);
	}

	void OnDestroy() {
		MouseActions.Instance.RemoveMoveable(this);
	}

	public void Move(Vector2 where) {
		stop();
		Debug.Log($"{gameObject} moving to {where}");
		target = where;
		origin = transform.position;
		direction = ((Vector2)target - (Vector2)origin).normalized;
		moving = true;
	}

	void Update() {
		if (moving) {
			// Accelerate till speedCap
			if (speed < speedCap) {
				speed += SpeedIncrement * speedCap;
				if (speed > speedCap)
					speed = speedCap;
			}
			lastSpeed = speed;
			transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
			// TODO: rotate
			// Stop when target is approached
			if (distance() < TargetDistanceDelta)
				stop();
		} else stop();
	}

	private void stop() {
		lastSpeed = speed = 0f;
		moving = false;
	}

	private float distance() {
		return Vector2.Distance(target, (Vector2)transform.position);
	}

	void OnTriggerStay2D(Collider2D other) {
		if (moving && other.gameObject.GetComponent<Moveable>().moving) 
			return;
		var d = (Vector2)transform.position - (Vector2)other.gameObject.transform.position;
		transform.position = (Vector2)transform.position + d * Mathf.Max(speedCap * SpeedIncrement, lastSpeed) * Time.deltaTime;
		stop();
	}
}

}
