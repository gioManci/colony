using System;
using System.Collections.Generic;
using UnityEngine;

namespace Colony
{
    public class TargetSystem
    {
        private GameObject owner;
        private List<GameObject> targets;

        public TargetSystem(GameObject owner)
        {
            this.owner = owner;
        }

        public bool HasTarget { get { return targets.Count > 0; } }

        public void Update()
        {

        }
    }
}
