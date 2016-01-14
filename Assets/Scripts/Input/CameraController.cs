using UnityEngine;
using System.Collections;
using Colony;
using Colony.UI;

namespace Colony.Input {

using Input = UnityEngine.Input;

public class CameraController : MonoBehaviour {

	enum Direction { UP, RIGHT, DOWN, LEFT };

	// TODO: make this adjustable by player?
	public float arrowSpeed = 8f;
	public float mouseSpeedMax = 16f;

	// If cursor moves past this threshold (near the screen boundaries),
	// camera will start moving in that direction. Both values are in
	// percentual values of the corresponding dimension.
	public float HorizScreenThreshold = 0.02f,
	             VertScreenThreshold = 0.02f;
	private Boundaries bounds;

	void Start() {
		bounds = GameObject.FindWithTag("Ground").GetComponent<Boundaries>();
	}

	// Update is called once per frame
	void Update() {
		if (UIController.Instance.MsgPrompt.IsActive)
			return;
		
		/// Arrows movements ///
		var pos = transform.position;
		var limit = bounds.worldSize/2f - arrowSpeed * Time.deltaTime * 2;
		if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && pos.y < limit) 
			transform.Translate(Vector2.up * arrowSpeed * Time.deltaTime, Space.World);
		if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && pos.y > -limit)
		         transform.Translate(Vector2.down * arrowSpeed * Time.deltaTime, Space.World);
		if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && pos.x > -limit) 
			transform.Translate(Vector2.left * arrowSpeed * Time.deltaTime, Space.World);
		if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && pos.x < limit)
			transform.Translate(Vector2.right * arrowSpeed * Time.deltaTime, Space.World);

		/// Mouse movements ///
		var mpos = Input.mousePosition;
		if (Screen.width - mpos.x < HorizScreenThreshold * Screen.width && pos.x < limit)
			transform.Translate(Vector2.right * calcSpeed(mpos, Direction.RIGHT) * Time.deltaTime, Space.World);
		else if (mpos.x < HorizScreenThreshold * Screen.width && pos.x > -limit)
			transform.Translate(Vector2.left * calcSpeed(mpos, Direction.LEFT) * Time.deltaTime, Space.World);
		if (Screen.height - mpos.y < VertScreenThreshold * Screen.height && pos.y < limit)
			transform.Translate(Vector2.up * calcSpeed(mpos, Direction.UP) * Time.deltaTime, Space.World);
		else if (mpos.y < VertScreenThreshold * Screen.height && pos.y > -limit)
			transform.Translate(Vector2.down * calcSpeed(mpos, Direction.DOWN) * Time.deltaTime, Space.World);

	}

	private float calcSpeed(Vector2 pos, Direction dir) {
		switch (dir) {
		case Direction.UP:
			return mouseSpeedMax * (pos.y - Screen.height * (1 - VertScreenThreshold))
				/ (Screen.height * VertScreenThreshold);
		case Direction.DOWN:
			return mouseSpeedMax * (Screen.height * VertScreenThreshold - pos.y)
				/ (Screen.height * VertScreenThreshold);
		case Direction.LEFT:
			return mouseSpeedMax * (Screen.width * HorizScreenThreshold - pos.x) 
				/ (Screen.width * HorizScreenThreshold);
		case Direction.RIGHT:
			return mouseSpeedMax * (pos.x - Screen.width * (1 - HorizScreenThreshold)) 
				/ (Screen.width * HorizScreenThreshold);
		}
		return 0f;
	}
}

}
