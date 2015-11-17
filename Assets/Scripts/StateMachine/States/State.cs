using UnityEngine;
using System.Collections;

public abstract class State
{
    public abstract void OnEnter(GameObject gameObject);
    public abstract void OnUpdate(GameObject gameObject);
    public abstract void OnExit(GameObject gameObject);
}