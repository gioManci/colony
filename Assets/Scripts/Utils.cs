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

	// Generate gaussian numbers using Box-Muller
	public static float NextGaussian(float mean, float stdDev) {
		float u1 = Random.Range(0, 1);
		float u2 = Random.Range(0, 1);
		// random normal(0, 1)
		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * (float)Math.PI * u2); 
		// random normal(mean, stdDev)
		return mean + stdDev * randStdNormal;
	}
}

}
