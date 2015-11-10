using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {
	public void Move(Vector2 where) {
		Debug.Log(gameObject + " moved to " + where);
		// TODO: some physics
		gameObject.transform.position = where;
	}
}
