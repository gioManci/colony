using Colony.Input;
using System;
using UnityEngine;

namespace Colony.Missions.SimpleMissions
{
    public class ClickOn : Mission
    {
        private string tag;

        public ClickOn(string title, string description, string tag) :
            base(title, description)
        {
            this.tag = tag;
        }

        public override void OnAccomplished()
        {
            
        }

        public override void OnActivate()
        {
            MouseActions.Instance.ObjectSelected += OnClick;
        }

        private void OnClick(GameObject obj)
        {
            if (obj.tag == tag)
            {
                MouseActions.Instance.ObjectSelected -= OnClick;
                NotifyCompletion(this);
            }
        }
    }
}
