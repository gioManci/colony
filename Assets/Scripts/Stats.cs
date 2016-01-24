using System;
using UnityEngine;
using Colony.Specializations;

namespace Colony
{
    public class Stats : MonoBehaviour
    {
        private Specialization spec;

        public float initialSpeed;
        public float initialVisualRadius;
        public int initialDamage;
        public float initialAttackRange;
        public float initialAttackCooldownTime;
        public float initialLoadSize;
        public float initialLoadTime;
        public float initialReactionTime;

        public static event Action<GameObject> BeeSpecialized;

        private float speed;
        private float visualRadius;
        private int damage;
        private float attackRange;
        private float attackCooldownTime;
        private float loadSize;
        private float loadTime;
        private float reactionTime;

	public Specialization Spec {
		get { return spec; }
		private set { spec = value; }
	}

        public float Speed
        {
            get
            {
                return speed * spec.SpeedMultiplier;
            }
        }

        public float VisualRadius
        {
            get
            {
                return visualRadius * spec.VisualRadiusMultiplier;
            }
        }

        public int Damage
        {
            get
            {
                return (int)(damage * spec.DamageMultiplier);
            }
        }

        public float AttackRange
        {
            get
            {
                return attackRange * spec.AttackRangeMultiplier;
            }
        }

        public float AttackCooldownTime
        {
            get
            {
                return attackCooldownTime * spec.CooldownTimeMultiplier;
            }
        }

        public float LoadSize
        {
            get
            {
                return loadSize * spec.LoadSizeMultiplier;
            }
        }

        public float LoadTime
        {
            get
            {
                return loadTime * spec.LoadTimeMultiplier;
            }
        }

        public float ReactionTime
        {
            get
            {
                return reactionTime * spec.ReactionTimeMultiplier;
            }
        }

        public bool IsSpecialized
        {
            get
            {
                return spec != null && spec.Type != SpecializationType.None;
            }
        }

        public SpecializationType Specialization { get { return spec.Type; } }

        void Awake()
        {
            spec = SpecializationFactory.GetSpecialization(SpecializationType.None);

            speed = initialSpeed;
            visualRadius = initialVisualRadius;
            damage = initialDamage;
            attackRange = initialAttackRange;
            attackCooldownTime = initialAttackCooldownTime;
            loadSize = initialLoadSize;
            loadTime = initialLoadTime;
            reactionTime = initialReactionTime;
        }

        public void Specialize(SpecializationType type)
        {
            if (type != SpecializationType.None)
            {
                //This should be changed
                if (type == SpecializationType.Inkeeper)
                {
                    gameObject.GetComponent<Controllable>().canInkeep = true;
                }

                spec = SpecializationFactory.GetSpecialization(type);
                GetComponent<Animator>().SetInteger("Specialization", (int)type);
                if (BeeSpecialized != null)
                {
                    BeeSpecialized(gameObject);
                }
            }
        }

        public void Unspecialize()
        {
            if (IsSpecialized)
            {
                spec = SpecializationFactory.GetSpecialization(SpecializationType.None);
                GetComponent<Animator>().SetInteger("Specialization", (int)SpecializationType.None);
            }
        }
    }
}