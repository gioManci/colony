using UnityEngine;
using System.Collections;
using Colony.Tasks.BasicTasks;
using Colony.Tasks.ComplexTasks;

namespace Colony.Tasks {

public class WorkerBeeBrain : MonoBehaviour
{
    private ComplexTask brain;

	void Start ()
	{
        brain = new Think(gameObject);
	}
	
	void Update ()
	{
        brain?.Process();
	}

    public void DoMove(Vector2 position)
    {
        brain.RemoveAllSubtasks();
        brain.AddSubtask(new Move(gameObject, position));
    }

    public void DoHarvest(GameObject resource)
    {

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
}

}
