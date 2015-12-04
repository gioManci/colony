using UnityEngine;
using System.Collections.Generic;
using Colony.Tasks;
using Colony.Resources;
using Colony;

namespace Colony.Input
{

    using Input = UnityEngine.Input;

    // Implements all the actions performed when an event is caught
    // by MouseMonitor.
    public class MouseActions : MonoBehaviour
    {

        private HashSet<Selectable> selected = new HashSet<Selectable>();
        private HashSet<Selectable> selectables = new HashSet<Selectable>();
        private HashSet<Controllable> controllables = new HashSet<Controllable>();

        private GameObject moveTarget;

        // Make this class a singleton
        public static MouseActions Instance { get; private set; }

        private MouseActions() { }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                MouseMonitor.OnLeftClick += clickSelect;
                MouseMonitor.OnDrag += dragSelect;
                MouseMonitor.OnRightClick += dispatchRightClick;
            }
            else
            {
                GameObject.Destroy(this);
            }
        }

        public void AddSelectable(Selectable obj)
        {
            selectables.Add(obj);
        }

        public void RemoveSelectable(Selectable obj)
        {
            selectables.Remove(obj);
        }

        public void AddControllable(Controllable obj)
        {
            controllables.Add(obj);
        }

        public void RemoveControllable(Controllable obj)
        {
            controllables.Remove(obj);
        }

        private void clickSelect(Click click)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                deselectAll();

            // Find out if some Selectable was hit by click
            var obj = Utils.GetObjectAt(click.pos);
            if (obj != null)
            {
                Selectable sel = obj.GetComponentInChildren<Selectable>();
                // If so, select it
                if (sel != null)
                {
                    sel.SelectToggle();
                    // If unit is controllable, add it to selected list.
                    if (obj.GetComponentInChildren<Controllable>() != null)
                    {
                        if (sel.IsSelected)
                            selected.Add(sel);
                        else
                            selected.Remove(sel);
                    }
                }
            }
        }

        private void dragSelect(Drag drag)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                deselectAll();

            foreach (Selectable obj in selectables)
            {
                var go = obj.gameObject;
                if (!go.GetComponent<Renderer>().isVisible) continue;
                if (drag.spanRect.Contains(go.transform.position))
                    obj.Select();
            }
        }

        private void dispatchRightClick(Click click)
        {
            if (selected.Count > 0)
            {
                // Check if click was on an interactable unit (resource, etc)
                var obj = Utils.GetObjectAt(click.pos);
                if (obj != null)
                {
                    if ("Flower".Equals(obj.tag))
                    {
                        foreach (Controllable bee in controllables)
                        {
                            Selectable sel = bee.GetComponent<Selectable>();
                            if (sel != null && sel.IsSelected)
                            {
                                WorkerBeeBrain brain = sel.GetComponent<WorkerBeeBrain>();
                                brain.DoHarvest(obj);
                            }
                        }
                    }
                    //moveSelectedUnits(click.pos);
                }
                else
                {
                    moveSelectedUnits(click.pos);
                }
            }
            else
            {
                moveSelectedUnits(click.pos);
            }
        }

        private void moveSelectedUnits(Vector2 pos)
        {
            // Draw a point on move target
            if (moveTarget != null)
                GameObject.Destroy(moveTarget);
            moveTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            moveTarget.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            moveTarget.transform.position = pos;

            // Move each selected moveable object
            foreach (Controllable obj in controllables)
            {
                Selectable sel = obj.gameObject.GetComponentInChildren<Selectable>();
                if (sel != null && sel.IsSelected)
                {
                    WorkerBeeBrain brain = obj.GetComponent<WorkerBeeBrain>();
                    brain.DoMove(pos);
                }
            }
        }

        private void deselectAll()
        {
            foreach (Selectable s in selectables)
            {
                Debug.Log(s);
                if (s == null)
                    selectables.Remove(s);
                else
                    s.Deselect();
            }
        }
    }
}
