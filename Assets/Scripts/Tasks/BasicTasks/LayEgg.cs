using System;
using UnityEngine;
using Colony.UI;
using Colony.Resources;

namespace Colony.Tasks.BasicTasks
{
    public class LayEgg : Task
    {
        private GameObject breedingCell;

        public static event Action<GameObject, GameObject> EggLaid;

        public LayEgg(GameObject agent, GameObject breedingCell) : base(agent, TaskType.LayEgg)
        {
            this.breedingCell = breedingCell;
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

            GameObject larva = EntityManager.Instance.CreateLarva(breedingCell.transform.position);
            larva.GetComponent<Larva>().BreedingCell = breedingCell;
	    UIController.Instance.resourceManager.RemoveResources(Costs.Larva);
            breedingCell.GetComponent<Cell>().CellState = Cell.State.CreateEgg;
            status = Status.Completed;
            if (EggLaid != null)
            {
                EggLaid(agent, larva);
            }

            return status;
        }

        public override void Terminate()
        {

        }
    }
}