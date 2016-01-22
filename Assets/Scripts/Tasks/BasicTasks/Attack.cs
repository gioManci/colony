using Colony.Tasks.BasicTasks;
using Colony.Behaviour;
using Colony.UI;
using Colony.Sounds;
using System;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class Attack : Task
    {
        private GameObject enemy;
        private SteeringBehaviour steeringBehaviour;
        private Stats stats;
        private float remainingCooldownTime;
	private bool warningGiven;

        public Attack(GameObject agent, GameObject enemy) : base(agent, TaskType.Attack)
        {
            this.enemy = enemy;
            steeringBehaviour = agent.GetComponent<SteeringBehaviour>();
            stats = agent.GetComponent<Stats>();
            remainingCooldownTime = 0.0f;
        }

        public override void Activate()
        {
            status = Status.Active;

            steeringBehaviour.StartPursuit(enemy);
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();
            remainingCooldownTime -= Time.deltaTime;

            if (enemy == null)
            {
                status = Status.Completed;
            }
            else
            {
                Vector2 toEnemy = enemy.transform.position - agent.transform.position;
                if (remainingCooldownTime <= 0 && toEnemy.sqrMagnitude < stats.AttackRange * stats.AttackRange)
                {
                    enemy.SendMessage("OnHit", agent);
                    remainingCooldownTime = stats.AttackCooldownTime;
                    if (!warningGiven && EntityManager.Instance.IsBee(agent))
                    {
                        TextController.Instance.Add("Your bees are being attacked!");
                        SoundEffects.Instance.Play(SoundEffects.Sound.Attacked);
                        warningGiven = true;
                        GameObject[] neighbors =
                            EntityManager.Instance.GetNearbyUnits(agent.transform.position, stats.VisualRadius);
                        foreach (GameObject unit in neighbors)
                        {
                            unit.SendMessage("OnHelp", enemy);
                        }
                    }
                }
            }

            return status;
        }

        public override void Terminate()
        {
            steeringBehaviour.StopPursuit();
        }
    }
}
