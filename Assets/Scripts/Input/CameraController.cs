using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	enum Direction { UP, RIGHT, DOWN, LEFT };

	// TODO: make this adjustable by player?
	private const float ARROW_SPEED = 8f;
	private const float MOUSE_SPEED_MAX = 16f;
	// If cursor moves past this threshold (near the screen boundaries),
	// camera will start moving in that direction. Both values are in
	// percentual values of the corresponding dimension.
	private const float HORIZ_SCREEN_THRESHOLD = 0.15f,
	                    VERT_SCREEN_THRESHOLD = 0.15f;

	// Update is called once per frame
	void Update() {
		/// Arrows movements ///
		if (Input.GetKey(KeyCode.UpArrow)) 
			transform.Translate(Vector2.up * ARROW_SPEED * Time.deltaTime, Space.World);
		if (Input.GetKey(KeyCode.DownArrow))
		         transform.Translate(Vector2.down * ARROW_SPEED * Time.deltaTime, Space.World);
		if (Input.GetKey(KeyCode.LeftArrow)) 
			transform.Translate(Vector2.left * ARROW_SPEED * Time.deltaTime, Space.World);
		if (Input.GetKey(KeyCode.RightArrow))
			transform.Translate(Vector2.right * ARROW_SPEED * Time.deltaTime, Space.World);

		/// Mouse movements ///
		var pos = Input.mousePosition;
		if (Screen.width - pos.x < HORIZ_SCREEN_THRESHOLD * Screen.width)
			transform.Translate(Vector2.right * calcSpeed(pos, Direction.RIGHT) * Time.deltaTime, Space.World);
		else if (pos.x < HORIZ_SCREEN_THRESHOLD * Screen.width)
			transform.Translate(Vector2.left * calcSpeed(pos, Direction.LEFT) * Time.deltaTime, Space.World);
		if (Screen.height - pos.y < VERT_SCREEN_THRESHOLD * Screen.height)
			transform.Translate(Vector2.up * calcSpeed(pos, Direction.UP) * Time.deltaTime, Space.World);
		else if (pos.y < VERT_SCREEN_THRESHOLD * Screen.height)
			transform.Translate(Vector2.down * calcSpeed(pos, Direction.DOWN) * Time.deltaTime, Space.World);

	}

	private float calcSpeed(Vector2 pos, Direction dir) {
		switch (dir) {
		case Direction.UP:
			return MOUSE_SPEED_MAX * (pos.y - Screen.height * (1 - VERT_SCREEN_THRESHOLD))
				/ (Screen.height * VERT_SCREEN_THRESHOLD);
		case Direction.DOWN:
			return MOUSE_SPEED_MAX * (Screen.height * VERT_SCREEN_THRESHOLD - pos.y)
				/ (Screen.height * VERT_SCREEN_THRESHOLD);
		case Direction.LEFT:
			return MOUSE_SPEED_MAX * (Screen.width * HORIZ_SCREEN_THRESHOLD - pos.x) 
				/ (Screen.width * HORIZ_SCREEN_THRESHOLD);
		case Direction.RIGHT:
			return MOUSE_SPEED_MAX * (pos.x - Screen.width * (1 - HORIZ_SCREEN_THRESHOLD)) 
				/ (Screen.width * HORIZ_SCREEN_THRESHOLD);
		}
		return 0f;
	}
}