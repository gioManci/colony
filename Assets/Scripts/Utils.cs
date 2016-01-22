using UnityEngine;
using System;

namespace Colony {

using Random = UnityEngine.Random;

public static class Utils {

	public static GameObject GetObjectAt(Vector2 pos) {
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		var collider = hit.collider;
		if (collider != null)
			return collider.gameObject;

		return null;
	}

//	// Generate gaussian numbers using Box-Muller
//	public static float NextGaussian(float mean, float stdDev) {
//		float u1 = Random.Range(0, 1);
//		float u2 = Random.Range(0, 1);
//		// random normal(0, 1)
//		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * (float)Math.PI * u2); 
//		// random normal(mean, stdDev)
//		return mean + stdDev * randStdNormal;
//	}
//
	// Returns true if the circle centered in c with radius r contains
	// position pos
//	public static bool CircleContains(Vector2 c, float r, Vector2 pos) {
//		Debug.Assert(r >= 0f, "Negative radius for CircleContains!");
//		return (c - pos).magnitude <= r;
//	}

	public static bool CircleIntersectsCircle(Vector2 c1, float r1, Vector2 c2, float r2) {
		Debug.Assert(r1 >= 0f && r2 >= 0f, "Invalid parameters for CircleIntersectsCircle! (negative radii)");
		var m = (c1 - c2).magnitude;
		return Mathf.Abs(r1 - r2) <= m && m <= r1 + r2;
	}
}

}
