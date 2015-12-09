using UnityEngine;
using System.Collections;
using Colony.Input;
using Colony.Tasks;
using Colony.Tasks.ComplexTasks;
using Colony.Tasks.BasicTasks;

namespace Colony
{
    using Move = Colony.Tasks.BasicTasks.Move;

    public class Controllable : MonoBehaviour
    {
        public bool canHarvest;
        public bool canAttack;
        public bool canBuild;
        public bool canBreed;
        public bool canRepair;
        public bool canMove;

        private ComplexTask brain;

        void Awake()
        {
            brain = new Think(gameObject);
        }

        void Update()
        {
            if (brain != null)
            {
                brain.Process();
            }
        }

        /// <summary>
        /// Tells the game object to move to the specified position.
        /// </summary>
        /// <param name="position">The position where to move.</param>
        public void DoMove(Vector2 position)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Move(gameObject, position));
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

        public void DoAttack()
        {

        }

        public void DoBreed(GameObject breedingCell)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Breed(gameObject, breedingCell));
        }

        // Use this for initialization
        void Start()
        {
            MouseActions.Instance.AddControllable(this);
        }

        void OnDestroy()
        {
            MouseActions.Instance.RemoveControllable(this);
        }
    }

}