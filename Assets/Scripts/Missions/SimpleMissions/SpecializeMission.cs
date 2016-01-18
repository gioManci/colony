using Colony.Specializations;
using UnityEngine;

namespace Colony.Missions.SimpleMissions
{
    public class SpecializeMission : Mission
    {
        private SpecializationType targetSpecialization;
        private int targetTimes;
        private int count;


        public SpecializeMission(string title, string description, SpecializationType specialization, int times) :
            base(title, description)
        {
            targetSpecialization = specialization;
            targetTimes = times;
            count = 0;
        }

        public override void OnAccomplished()
        {
            
        }

        public override void OnActivate()
        {
            Stats.BeeSpecialized += OnBeeSpecialized;
        }

        private void OnBeeSpecialized(GameObject bee)
        {
            if (bee.GetComponent<Stats>().Specialization == targetSpecialization)
            {
                if (++count >= targetTimes)
                {
                    Stats.BeeSpecialized -= OnBeeSpecialized;
                    NotifyCompletion(this);
                }
            }
        }
    }
}
