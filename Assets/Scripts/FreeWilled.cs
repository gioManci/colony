using UnityEngine;
using Colony.Tasks;
using Colony.Tasks.ComplexTasks;

namespace Colony
{
    public class FreeWilled : MonoBehaviour
    {
        private ComplexTask brain;

        void Awake()
        {
            brain = new Arbitrate(gameObject);
        }

        void Update()
        {
            if (brain != null)
            {
                brain.Process();
            }
        }
    }
}