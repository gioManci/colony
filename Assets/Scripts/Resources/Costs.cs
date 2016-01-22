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
						.With(ResourceType.Water, 5)
						.With(ResourceType.RoyalJelly, 1);
	public static ResourceSet NewHive = new ResourceSet()
						.With(ResourceType.Pollen, 1500)
						.With(ResourceType.Honey, 700)
						.With(ResourceType.RoyalJelly, 300)
						.With(ResourceType.Water, 1000);
	public static ResourceSet SpecGuard = new ResourceSet()
						.With(ResourceType.Pollen, 25)
						.With(ResourceType.Honey, 15)
						.With(ResourceType.Water, 25);
	public static ResourceSet SpecForager = new ResourceSet()
						.With(ResourceType.Pollen, 25)
						.With(ResourceType.Honey, 15)
						.With(ResourceType.Water, 25);
	public static ResourceSet SpecInkeeper = new ResourceSet()
						.With(ResourceType.Pollen, 25)
						.With(ResourceType.Honey, 15)
						.With(ResourceType.Water, 25);

	public static ResourceSet Get(string key) {
		switch (key) {
		case "WorkerBee": return WorkerBee;
		case "QueenBee": return QueenBee;
		case "DroneBee": return DroneBee;
		case "Larva": return Larva;
		case "NewHive": return NewHive;
		case "SpecGuard": return SpecGuard;
		case "SpecForager": return SpecForager;
		case "SpecInkeeper": return SpecInkeeper;
		}
		return null;
	}
}

}
