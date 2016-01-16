using UnityEngine;
using Colony.Tasks;
using Colony.Tasks.ComplexTasks;
using Colony.Tasks.BasicTasks;

namespace Colony
{
    public class FreeWilled : MonoBehaviour
    {
	public ComplexTask brain { get; private set; }

        void Awake()
        {
            brain = new Think(gameObject);
            brain.AddSubtask(new Arbitrate(gameObject));
        }

        void Update()
        {
            if (brain != null)
            {
                brain.Process();
            }
        }

	public void DoMove(Vector2 position)
	{
            brain.AddSubtask(new Move(gameObject, position, 0.5f));
	}
    }
}