using UnityEngine;
using System.Collections;

namespace Colony.Resources {

public abstract class ResourceYielder {
	protected ResourceYielder(ResourceSet resources) {
		this.resources = resources;
	}
	
	public ResourceSet Yield() {
		var rs = new ResourceSet();
		for (int i = 0; i < resources.Length; ++i) {
			if (resources[i] > 0) {
				++rs[i];
				--resources[i];
			}
		}
		return rs;
	}
	
	protected ResourceSet resources;
}

}