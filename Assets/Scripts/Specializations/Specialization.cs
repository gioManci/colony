namespace Colony.Specializations
{
    public abstract class Specialization
    {
        protected Specialization(
            SpecializationType type,
            float speedMultiplier,
            float visualRadiusMultiplier,
            float damageMultiplier,
            float attackRangeMultiplier,
            float cooldownTimeMultiplier,
            float loadSizeMultiplier,
            float loadTimeMultiplier,
            float reactionTimeMultiplier)
        {
            Type = type;
            SpeedMultiplier = speedMultiplier;
            VisualRadiusMultiplier = visualRadiusMultiplier;
            DamageMultiplier = damageMultiplier;
            AttackRangeMultiplier = attackRangeMultiplier;
            CooldownTimeMultiplier = cooldownTimeMultiplier;
            LoadSizeMultiplier = loadSizeMultiplier;
            LoadTimeMultiplier = loadTimeMultiplier;
            ReactionTimeMultiplier = reactionTimeMultiplier;
        }

        public SpecializationType Type { get; private set; }

        public float SpeedMultiplier { get; private set; }
        public float VisualRadiusMultiplier { get; private set; }
        public float DamageMultiplier { get; private set; }
        public float AttackRangeMultiplier { get; private set; }
        public float CooldownTimeMultiplier { get; private set; }
        public float LoadSizeMultiplier { get; private set; }
        public float LoadTimeMultiplier { get; private set; }
        public float ReactionTimeMultiplier { get; private set; }
    }
}
