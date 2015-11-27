using UnityEngine;
using System.Collections;

namespace Colony.Input {

// Represents a mouse click
public struct Click {
	public Click(Vector2 pos) {
		this.pos = pos;
	}
	public readonly Vector2 pos;  
}

// Represents a mouse drag
public struct Drag {
	public Drag(Vector2 start, Vector2 end) {
		this.start = start;
		this.end = end;
		this.span = (start - end).magnitude;
		this.spanRect = new Rect(Mathf.Min(start.x, end.x), Mathf.Min(start.y, end.y),
				    Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
	}
	public readonly Vector2 start, end;
	public readonly float span;
	public readonly Rect spanRect;
}

}
