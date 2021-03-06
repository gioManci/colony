using UnityEngine;

namespace Colony.UI {

using UCursor = UnityEngine.Cursor;

public class Cursor : MonoBehaviour {
	public enum Type {
		Normal, Click, Attack
	}

	public Texture2D normalCursorTexture;
	public Texture2D clickCursorTexture;
	public Texture2D attackCursorTexture;

	private Type cursor = Type.Normal;

	public Type CursType {
		get { return cursor; }
	}

	// Make this class a singleton
	public static Cursor Instance { get; private set; }

	private Cursor() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Start() {
		UCursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.Auto);
	}

	public void SetCursor(Type type) {
		cursor = type;

		switch (type) {
		case Type.Normal:
			UCursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.Auto);
			break;
		case Type.Click:
			UCursor.SetCursor(clickCursorTexture, Vector2.zero, CursorMode.Auto);
			break;
		case Type.Attack:
			UCursor.SetCursor(attackCursorTexture, Vector2.zero, CursorMode.Auto);
			break;
		}
	}
}

}
