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
                1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f) { }
        }

        private class Guard : Specialization
        {
            public Guard() : base(
                SpecializationType.Guard,
                1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f) { }
        }

        private class Forager : Specialization
        {
            public Forager() : base(
                SpecializationType.Forager,
                1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f) { }
        }

        private class Inkeeper : Specialization
        {
            public Inkeeper() : base(
                SpecializationType.Inkeeper,
                1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f) { }
        }

        private class Scout : Specialization
        {
            public Scout() : base(
                SpecializationType.Scout,
                1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f) { }
        }
    }
}
