using UnityEngine;
using System;
using Colony.Missions;

// Workaround workaround workaround
namespace Colony.Missions.SimpleMissions {

public class ActivateObject : Mission {

	private bool activate;
	private GameObject obj;

	public ActivateObject(GameObject obj, bool activate = true) : base("", "") {
		this.obj = obj;
		this.activate = activate;
	}

        public override void Dispose()
        {
            
        }

        public override void OnActivate() {
		obj.SetActive(activate);
		NotifyCompletion(this);
	}
}

}