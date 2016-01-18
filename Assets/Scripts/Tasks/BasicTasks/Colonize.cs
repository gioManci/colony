using System;
using UnityEngine;
using Colony.UI;
using Colony.Hive;

namespace Colony.Tasks.BasicTasks
{

using Hive = Colony.Hive.Hive;

    class Colonize : Task
    {
        public Colonize(GameObject agent) : base(agent, TaskType.Colonize)
        {

        }

        public override void Activate()
        {
            status = Status.Active;
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (canCreateHive(agent.transform.position))
                EntityManager.Instance.CreateBeehive(agent.transform.position);
            else
                TextController.Instance.Add("Cannot create a hive here!");
		
            return status = Status.Completed;
        }

        public override void Terminate()
        {
            
        }

	private delegate float CalcHiveRadius(uint i);

	private bool canCreateHive(Vector2 pos) {
		// Get the default hive
		var mainHive = EntityManager.Instance.Beehives[0].GetComponent<Hive>();

		float cellRadius = mainHive.CellTemplate.GetComponent<SpriteRenderer>().bounds.size.x;
		CalcHiveRadius hiveRadius = (r) => (r + 0.5f) * cellRadius;

		float oneCellHiveRadius = hiveRadius(1);

		// Prevent overlapping with other beehives
		foreach (var hiveObj in EntityManager.Instance.Beehives) {
			if (Utils.CircleIntersectsCircle(
				    pos, oneCellHiveRadius,
				    hiveObj.transform.position, hiveRadius(hiveObj.GetComponent<Hive>().Radius))) {
				return false;
			}
		}
		// Prevent overlapping with resources
		foreach (var resObj in EntityManager.Instance.Resources) {
			if (Utils.CircleIntersectsCircle(
				    pos, oneCellHiveRadius,
				    resObj.transform.position, resObj.GetComponent<SpriteRenderer>().bounds.size.x))
				return false;
		}

		return true;
	}
    }
}
