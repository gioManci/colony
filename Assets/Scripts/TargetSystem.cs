using Colony.Resources;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Colony
{
    public class TargetSystem
    {
        private GameObject owner;
        private Stats stats;
        private List<GameObject> targets;

        public TargetSystem(GameObject owner)
        {
            this.owner = owner;
            targets = new List<GameObject>();
            CurrentTarget = null;
            stats = owner.GetComponent<Stats>();
        }

        public bool HasTarget { get { return CurrentTarget != null; } }

        public GameObject CurrentTarget { get; private set; }

        public float LastUpdate { get; private set; }

        public void Update()
        {
            LastUpdate = Time.time;

            targets.Clear();
            targets.AddRange(EntityManager.Instance.GetNearbyUnits(owner.transform.position, stats.VisualRadius));
            targets.AddRange(EntityManager.Instance.GetNearbyCells(owner.transform.position, stats.VisualRadius));
            targets.Remove(owner);
            if (targets.Count == 0)
            {
                CurrentTarget = null;
            }
            else
            {
                CurrentTarget = FindBestTarget();
            }
        }

        /// <summary>
        /// Returns the best suited target for the agent. It could either be a bee or a cell.
        /// </summary>
        /// <returns>The best target.</returns>
        private GameObject FindBestTarget()
        {
            GameObject targetBee = null;
            GameObject targetCell = null;
            Vector2 distanceToClosest = Vector2.zero;

            //Since in targets the bees are inserted always before the cells AND (for the moment) bees
            //have a higher priority than the cells, if we find a bee, we stop the search.
            foreach (GameObject entity in targets)
            {
                if (EntityManager.Instance.IsBee(entity))
                {
                    //Choose the bee with less life as target
                    if (targetBee == null)
                    {
                        targetBee = entity;
                    }
                    else if (targetBee.GetComponent<Life>().CurrentLife
                        > entity.GetComponent<Life>().CurrentLife)
                    {
                        targetBee = entity;
                    }
                }
                else
                {
                    //To save time, interrupt the search if a bee is already selected as target (see foreach comment)
                    if (targetBee != null)
                    {
                        return targetBee;
                    }
                    //Choose the closest cell
                    if (targetCell == null)
                    {
                        //Check if its hive contains resources
                        HiveWarehouse warehouse = entity.GetComponentInParent<HiveWarehouse>();
                        if (!warehouse.IsEmpty)
                        {
                            targetCell = entity;
                            distanceToClosest = targetCell.transform.position - owner.transform.position;
                        }
                    }
                    else
                    {
                        Vector2 distanceToCheck = entity.transform.position - owner.transform.position;
                        if (distanceToClosest.sqrMagnitude > distanceToCheck.sqrMagnitude)
                        {
                            //Add as target only if its hive contains some resources
                            HiveWarehouse warehouse = entity.GetComponentInParent<HiveWarehouse>();
                            if (!warehouse.IsEmpty)
                            {
                                targetCell = entity;
                                distanceToClosest = distanceToCheck;
                            }
                        }
                    }
                }
            }
            return targetCell;
        }
    }
}
