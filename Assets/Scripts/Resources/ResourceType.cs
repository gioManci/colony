namespace Colony.Resources {

public enum ResourceType {
	// raw
	Water,
	Pollen,
	Nectar,
	// refined
	Honey,
	RoyalJelly,
	Beeswax
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

	public static string ToString(this ResourceType type) {
		switch (type) {
		case ResourceType.Pollen:
			return "Pollen";
		case ResourceType.Nectar:
			return "Nectar";
		case ResourceType.Water:
			return "Water";
		case ResourceType.Honey:
			return "Honey";
		case ResourceType.RoyalJelly:
			return "RoyalJelly";
		case ResourceType.Beeswax:
			return "Beeswax";
		default:
			return "";
		}
	}

	public static string ToBrief(this ResourceType type) {
		switch (type) {
		case ResourceType.Pollen:
			return "P";
		case ResourceType.Nectar:
			return "N";
		case ResourceType.Water:
			return "W";
		case ResourceType.Honey:
			return "H";
		case ResourceType.RoyalJelly:
			return "RJ";
		case ResourceType.Beeswax:
			return "B";
		default:
			return "";
		}
	}
}
}