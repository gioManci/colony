using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Colony.UI {

public class FogOfWar : MonoBehaviour {

	// Use this for initialization
	void Start() {
		var texture = GetComponent<RawImage>().texture as Texture2D;
		for (int i = 0; i < 30; ++i)
			for (int j = 0; j < 100; ++j)
				texture.SetPixel(i, j, new Color(0f, 0f, 0f, 0f));
		texture.Apply();
	}

	public void Reveal(Vector2 center, float radius) {
		// TODO
	}
}

}