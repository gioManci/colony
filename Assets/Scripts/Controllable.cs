using UnityEngine;
using Colony.Tasks;
using Colony.Tasks.ComplexTasks;
using Colony.Tasks.BasicTasks;
using Colony.UI;
using Colony.Resources;

namespace Colony
{
    using Move = Colony.Tasks.BasicTasks.Move;

    [RequireComponent(typeof(Selectable))]
    public class Controllable : MonoBehaviour
    {
        public bool canHarvest;
        public bool canAttack;
        public bool canBuild;
        public bool canBreed;
        public bool canRepair;
        public bool canMove;
        public bool canColonize;

        private ComplexTask brain;

        void Awake()
        {
            brain = new Think(gameObject);
        }
	
	void Start() {
		if (gameObject.tag == "QueenBee") {
			var sel = GetComponent<Selectable>();
			sel.OnSelect += () => UIController.Instance.SetBottomPanel(UIController.BPType.Queen);
			sel.OnDeselect += () => UIController.Instance.SetBottomPanel(UIController.BPType.None);
		} else if (gameObject.tag == "WorkerBee") {
			var sel = GetComponent<Selectable>();
			sel.OnSelect += () => {
				UIController.Instance.SetBeeLoadText(GetComponent<BeeLoad>());
				var type = UIController.BPType.Text;
				if (GetComponent<Stats>().Spec.Type == Colony.Specializations.SpecializationType.None)
					type |= UIController.BPType.Spec;
				UIController.Instance.SetBottomPanel(type);
			};
			sel.OnDeselect += () => UIController.Instance.SetBottomPanel(UIController.BPType.None);
		}
	}

        void Update()
        {
            if (brain != null)
            {
                brain.Process();
            }
        }

	public bool IsInactive() {
		return brain.CurrentSubtask == null;
	}

        /// <summary>
        /// Tells the game object to move to the specified position.
        /// </summary>
        /// <param name="position">The position where to move.</param>
        public void DoMove(Vector2 position)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Move(gameObject, position, 0.5f));
        }

        /// <summary>
        /// Tells the game object to harvest from the specified resource.
        /// </summary>
        /// <param name="resource">The resource to harvest from.</param>
        public void DoHarvest(GameObject resource)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Harvest(gameObject, resource));
        }

        public void DoBuild()
        {
        }

        public void DoRepair()
        {
        }

        public void DoAttack(GameObject enemy)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Attack(gameObject, enemy));
        }

        public void DoBreed(GameObject breedingCell)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Breed(gameObject, breedingCell));
        }

        public void DoColonize()
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Colonize(gameObject));
        }

        public void OnHit(GameObject enemy)
        {
            if (canAttack)
            {
                if (!brain.IsCurrentSubtask(TaskType.Attack))
                {
                    brain.AddSubtask(new Attack(gameObject, enemy));
                }
            }
            else
            {
                //Runaway!!!
            }
        }
    }
}
