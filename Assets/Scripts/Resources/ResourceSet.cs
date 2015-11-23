using UnityEngine;
using System;
using System.Collections;

namespace Colony.Resources {

public class ResourceSet {
	public ResourceSet() {}
	
	public ResourceSet(ResourceSet other) {
		for (int i = 0; i < other.Length; ++i)
			resources[i] = other[i];
	}

	public ResourceSet With(ResourceType type, int amount) {
		resources[(int)type] += amount;
		return this;
	}
	
	public static ResourceSet operator -(ResourceSet rs, int amt) {
		var newrs = new ResourceSet(rs);
		for (int i = 0; i < newrs.Length; ++i) {
			newrs[i] = Mathf.Max(0, newrs[i] - amt);
		}
		return newrs;
	}
	
	public int Length {
		get { return resources.Length; }
	}
	
	public int this[int i] {
		get { return resources[i]; }
		set { resources[i] = value; }
	}

	private int[] resources = new int[Enum.GetNames(typeof(ResourceType)).Length];
}

}