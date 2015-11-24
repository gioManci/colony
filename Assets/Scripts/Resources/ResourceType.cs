namespace Colony.Resources {

public enum ResourceType {
	Pollen,
	Nectar,
	Water,
	Honey,
	RoyalJelly
}

public static class Extensions {
	public static bool IsRaw(this ResourceType type) {
		switch (type) {
		case ResourceType.Pollen:
		case ResourceType.Nectar:
		case ResourceType.Water:
			return true;
		default:
			return false;
		}
	}
}

}