namespace Colony.Resources
{

    public enum ResourceType
    {
        // raw
        Pollen,
        Water,
	Nectar,
        // refined
        Honey,
        RoyalJelly,
        Beeswax
    }

    public static class Extensions
    {
        public static bool IsRaw(this ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Pollen:
                case ResourceType.Nectar:
                case ResourceType.Water:
                    return true;
                default:
                    return false;
            }
        }

        public static string ToString(this ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Pollen: return "Pollen";
                case ResourceType.Nectar: return "Nectar";
                case ResourceType.Water: return "Water";
                case ResourceType.Honey: return "Honey";
                case ResourceType.RoyalJelly: return "RoyalJelly";
                case ResourceType.Beeswax: return "Beeswax";
                default: return "";
            }
        }
    }
}