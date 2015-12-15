namespace Colony.Resources {

public static class Costs {
	public static ResourceSet WorkerBee = new ResourceSet()
						.With(ResourceType.Pollen, 50)
						.With(ResourceType.Honey, 25)
						.With(ResourceType.Water, 50);
	public static ResourceSet QueenBee = new ResourceSet()
						.With(ResourceType.Pollen, 0)
						.With(ResourceType.Honey, 0)
						.With(ResourceType.Water, 300)
						.With(ResourceType.RoyalJelly, 100);
	public static ResourceSet DroneBee = new ResourceSet()
						.With(ResourceType.Pollen, 0)
						.With(ResourceType.Honey, 0)
						.With(ResourceType.Water, 0);
	public static ResourceSet Larva = new ResourceSet()
						.With(ResourceType.Pollen, 5)
						.With(ResourceType.Honey, 2)
						.With(ResourceType.Water, 5);
	public static ResourceSet NewHive = new ResourceSet()
						.With(ResourceType.Pollen, 2000)
						.With(ResourceType.Honey, 1000)
						.With(ResourceType.Water, 2000);

	public static ResourceSet Get(string key) {
		switch (key) {
		case "WorkerBee": return WorkerBee;
		case "QueenBee": return QueenBee;
		case "DroneBee": return DroneBee;
		case "Larva": return Larva;
		case "NewHive": return NewHive;
		}
		return null;
	}
}

}
