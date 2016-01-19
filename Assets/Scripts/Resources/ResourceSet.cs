using UnityEngine;
using System;
using System.Collections;

namespace Colony.Resources {
public class ResourceSet {
	public ResourceSet() {
	}

	public ResourceSet With(ResourceType type, int amount) {
		resources[(int)type] += amount;
		return this;
	}

	public static ResourceSet operator -(ResourceSet rs1, ResourceSet rs2) {
		ResourceSet result = new ResourceSet();
		foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			result[type] = Mathf.Max(0, rs1[type] - rs2[type]);
		}
		return result;
	}

	public static ResourceSet operator +(ResourceSet rs1, ResourceSet rs2) {
		ResourceSet result = new ResourceSet();
		foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			result[type] = Mathf.Max(0, rs1[type] + rs2[type]);
		}
		return result;
	}

	public static ResourceSet operator *(ResourceSet rs1, float mult) {
		ResourceSet result = new ResourceSet();
		foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			result[type] = (int)(result[type] * mult);
		}
		return result;
	}

	public int this[ResourceType i] {
		get { return resources[(int)i]; }
		set { resources[(int)i] = Mathf.Max(0, value); }
	}

	public bool IsEmpty() {
		for (int i = 0; i < resources.Length; i++) {
			if (resources[i] > 0) {
				return false;
			}
		}
		return true;
	}

	public int GetSize() {
		int size = 0;

		for (int i = 0; i < resources.Length; i++) {
			size += resources[i];
		}

		return size;
	}

	public void Clear() {
		for (int i = 0; i < resources.Length; i++) {
			resources[i] = 0;
		}
	}

	public override string ToString() {
		string s = "";
		for (int i = 0; i < resources.Length; ++i) {
			if (resources[i] > 0) {
				s += resources[i] + " " + ((ResourceType)i).ToBrief() + " / ";
			}
		}
		return s.Length >= 3 ? s.Substring(0, s.Length - 3) : s;
	}

	private int[] resources = new int[Enum.GetNames(typeof(ResourceType)).Length];
}
}
