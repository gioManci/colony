using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Colony {

public class FogOfWar : MonoBehaviour {

	private const float APPLY_INTERVAL = 0.2f;
	private Texture2D texture;
	private float count = APPLY_INTERVAL;
	private Boundaries bounds;

	// Use this for initialization
	void Start() {
		texture = GetComponent<RawImage>().texture as Texture2D;
		bounds = GameObject.Find("Ground").GetComponent<Boundaries>();
	}

	public void Reveal(Vector2 center, float radius) {
//		Debug.Log("Revealing");
//		// Convert world coordinates to pixel coordinates
//		// NOTE: assuming 100 pixel per unit
		Vector2 pixels = new Vector2(center.x + texture.width / 2f,
			center.y + texture.height / 2f);

		Debug.Log(pixels);
		Debug.Log( Mathf.Max(0, (int)(pixels.x - 20 * radius)) + ", " + (int)(pixels.x + 20 * radius));
		for (int i = Mathf.Max(0, (int)(pixels.x - 20 * radius)); i < (int)(pixels.x + 20 * radius); ++i)
//			Debug.Log(i);
			for (int j = Mathf.Max(0, (int)(pixels.y - 20 * radius)); j < (int)(pixels.y + 20 * radius); ++j)
//			Debug.Log(j);
//				Debug.Log(i + ", " + j);
					texture.SetPixel(i, j, new Color(0f, 0f, 0f, 0f));

//		count -= Time.deltaTime;
//		if (count <= 0) {
			texture.Apply();
//			count = APPLY_INTERVAL;
//		}
	}
}

}