using UnityEngine;
using System.Collections;

namespace InputEvents {
	public struct Click {
		public Click(Vector2 pos) {
			Pos = pos;
		}
		public readonly Vector2 Pos; 
	}

	public struct Drag {
		public Drag(Vector2 start, Vector2 end) {
			Start = start;
			End = end;
			Span = (start - end).magnitude;
			SpanRect = new Rect(Mathf.Min(start.x, end.x), Mathf.Min(start.y, end.y),
			                    Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
		}
		public readonly Vector2 Start, End;
		public readonly float Span;
		public readonly Rect SpanRect;
	}
}