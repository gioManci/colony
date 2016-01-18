using UnityEngine;

namespace Colony.Missions.SimpleMissions
{
    public class KillEnemies : Mission
    {
        private int killingCount;
        private int killingsToReach;
        private string enemyTag;

        public KillEnemies(string title, string description, string enemyTag, int number) :
            base(title, description)
        {
            killingCount = 0;
            killingsToReach = number;
            this.enemyTag = enemyTag;
        }

        public override void OnAccomplished()
        {
            
        }

        public override void OnActivate()
        {
            EntityManager.Instance.DestroyingEnemy += OnEnemyDestroyed;
        }

        private void OnEnemyDestroyed(GameObject enemy)
        {
            if (enemyTag == null || enemy.tag == enemyTag)
            {
                killingCount++;
            }
            if (killingCount >= killingsToReach)
            {
                EntityManager.Instance.DestroyingEnemy -= OnEnemyDestroyed;
                NotifyCompletion(this);
            }
        }
    }
}
