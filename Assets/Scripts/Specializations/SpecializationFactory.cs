using System;

namespace Colony.Specializations
{
    public static class SpecializationFactory
    {
        private static Specialization[] specializations =
            new Specialization[Enum.GetValues(typeof(SpecializationType)).Length];

        public static Specialization GetSpecialization(SpecializationType type)
        {
            if (specializations[(int)type] == null)
            {
                switch (type)
                {
                    case SpecializationType.None:
                        specializations[(int)type] = new None();
                        break;
                    case SpecializationType.Forager:
                        specializations[(int)type] = new Forager();
                        break;
                    case SpecializationType.Guard:
                        specializations[(int)type] = new Guard();
                        break;
                    case SpecializationType.Inkeeper:
                        specializations[(int)type] = new Inkeeper();
                        break;
                    case SpecializationType.Scout:
                        specializations[(int)type] = new Scout();
                        break;
                    default:
                        break;
                }
            }
            return specializations[(int)type];
        }

        private class None : Specialization
        {
            public None() : base(
                SpecializationType.None,
			speedMultiplier: 1.0f,
			visualRadiusMultiplier: 1.0f,
			damageMultiplier: 1.0f,
			attackRangeMultiplier: 1.0f,
			cooldownTimeMultiplier: 1.0f,
			loadSizeMultiplier: 1.0f,
			loadTimeMultiplier: 1.0f,
			reactionTimeMultiplier: 1.0f) { }
        }

        private class Guard : Specialization
        {
            public Guard() : base(
                SpecializationType.Guard,
                	speedMultiplier: 1.0f,
			visualRadiusMultiplier: 2.0f,
			damageMultiplier: 1.5f,
			attackRangeMultiplier: 1.0f,
			cooldownTimeMultiplier: 0.8f,
			loadSizeMultiplier: 0.5f,
			loadTimeMultiplier: 2f,
			reactionTimeMultiplier: 1.0f) { }
        }

        private class Forager : Specialization
        {
            public Forager() : base(
                SpecializationType.Forager,
			speedMultiplier: 1.5f,
			visualRadiusMultiplier: 1.0f,
			damageMultiplier: 0.5f,
			attackRangeMultiplier: 1.0f,
			cooldownTimeMultiplier: 1.0f,
			loadSizeMultiplier: 2.0f,
			loadTimeMultiplier: 0.8f,
			reactionTimeMultiplier: 1.0f) { }
        }

        private class Inkeeper : Specialization
        {
            public Inkeeper() : base(
                SpecializationType.Inkeeper,
			speedMultiplier: 0.5f,
			visualRadiusMultiplier: 2.0f,
			damageMultiplier: 0.75f,
			attackRangeMultiplier: 1.0f,
			cooldownTimeMultiplier: 1.0f,
			loadSizeMultiplier: 0.75f,
			loadTimeMultiplier: 1.0f,
			reactionTimeMultiplier: 1.0f) { }
        }

        private class Scout : Specialization
        {
            public Scout() : base(
                SpecializationType.Scout,
			speedMultiplier: 1.0f,
			visualRadiusMultiplier: 1.0f,
			damageMultiplier: 1.0f,
			attackRangeMultiplier: 1.0f,
			cooldownTimeMultiplier: 1.0f,
			loadSizeMultiplier: 1.0f,
			loadTimeMultiplier: 1.0f,
			reactionTimeMultiplier: 1.0f) { }
        }
    }
}
