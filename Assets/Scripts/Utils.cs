using UnityEngine;

namespace Colony {

public static class Utils {

	public static GameObject GetObjectAt(Vector2 pos) {
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		var collider = hit.collider;
		if (collider != null)
			return collider.gameObject;

		return null;
	}
}

}
