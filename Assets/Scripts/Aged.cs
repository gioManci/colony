using UnityEngine;
using System.Collections;

public class Aged : MonoBehaviour
{

    public float startingAge;
    public float Age { private set; get; }
	
    void Start()
    {
        Age = startingAge;
    }

	void Update ()
	{
        Age -= Time.deltaTime;
        if (Age < 0)
        {
            // Add code to handle destruction
            Destroy(gameObject);
        }
	}
}
