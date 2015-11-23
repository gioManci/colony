using UnityEngine;
using System.Collections;

namespace Colony.Resources {

public class Flower : ResourceYielder {
	public Flower() : base(new ResourceSet()
				.With(ResourceType.Nectar, 25)
				.With(ResourceType.Pollen, 25) 
				.With(ResourceType.Water, 25)
				) {}
}

}