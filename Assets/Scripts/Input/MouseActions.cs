using UnityEngine;
using System.Collections.Generic;
using Colony.Tasks;
using Colony.Resources;
using Colony;
using Colony.Hive;

namespace Colony.Input
{
    using Input = UnityEngine.Input;
    using Hive = Colony.Hive.Hive;

    // Implements all the actions performed when an event is caught
    // by MouseMonitor.
    public class MouseActions : MonoBehaviour
    {
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
		MouseMonitor.OnMove += changeCursor;
            }
            else
            {
                GameObject.Destroy(this);
            }
        }

	/************ Private ************/

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
                }
            }
        }

        private void dragSelect(Drag drag)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                deselectAll();

            foreach (var bee in EntityManager.Instance.Bees)
            {
		Selectable obj = bee.GetComponent<Selectable>();
		if (!obj.dragSelectable) continue;

                if (!bee.GetComponent<Renderer>().isVisible) continue;

                if (drag.spanRect.Contains(bee.transform.position)) {
                    obj.Select();
		}
            }
        }

        private void dispatchRightClick(Click click)
        {
                // Check if click was on an interactable unit (resource, etc)
                var obj = Utils.GetObjectAt(click.pos);
                if (obj != null)
                {
                    if ("Flower".Equals(obj.tag))
                    {
                        foreach (var b in EntityManager.Instance.Bees)
                        {
                            Selectable sel = b.GetComponent<Selectable>();
			    Controllable bee = b.GetComponent<Controllable>();
                            if (sel != null && sel.IsSelected && bee.canHarvest)
                            {
                                bee.DoHarvest(obj);
                            }
                        }
                    }
                    else if ("Cell".Equals(obj.tag))
                    {
                        foreach (var b in EntityManager.Instance.Bees)
                        {
                            Selectable sel = b.GetComponent<Selectable>();
			    Controllable bee = b.GetComponent<Controllable>();
                            if (sel != null && sel.IsSelected)
                            {
			        if (bee.canBreed)
			            bee.DoBreed(obj);
		                else if (bee.canMove)
				    bee.DoMove(click.pos);
                            }
                        }
                    }

                    if ("Wasp".Equals(obj.tag))
                    {
                        foreach (var b in EntityManager.Instance.Bees)
                        {
                            Selectable sel = b.GetComponent<Selectable>();
			    Controllable bee = b.GetComponent<Controllable>();
                            if (sel != null && sel.IsSelected && bee.canAttack)
                            {
                                bee.DoAttack(obj);
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
	
	private void moveSelectedUnits(Vector2 pos)
        {
            // Draw a point on move target
            if (moveTarget != null)
                GameObject.Destroy(moveTarget);
            moveTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            moveTarget.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            moveTarget.transform.position = pos;

            // Move each selected moveable object
            foreach (var b in EntityManager.Instance.Bees)
            {
		Controllable bee = b.GetComponent<Controllable>();
                Selectable sel = b.GetComponent<Selectable>();
		if (bee.canMove && sel.IsSelected)
                {
                    bee.DoMove(pos);
                }
            }
        }

        private void deselectAll()
        {
            foreach (var s in EntityManager.Instance.Bees)
            {
		s.GetComponent<Selectable>().Deselect();
            }
            foreach (var s in EntityManager.Instance.Resources)
            {
		s.GetComponent<Selectable>().Deselect();
            }
            foreach (var s in EntityManager.Instance.Beehives)
            {
		s.GetComponent<Hive>().DeselectAll();
            }
        }

	private void changeCursor(Move move) {
		bool beeSelected = false;
		foreach (var sel in EntityManager.Instance.Bees) {
			if (sel.tag == "Bee") {
				beeSelected = true;
				break;
			}
		}

		// TODO: add canHarvest check
		if (beeSelected) {
			var obj = Utils.GetObjectAt(move.pos);
			if (obj != null && obj.GetComponentInChildren<ResourceYielder>() != null) {
				Cursor.Instance.setCursor(Cursor.Type.Click); 
				return;
			}
		} 
		if (Cursor.Instance.CursType != Cursor.Type.Normal)
			Cursor.Instance.setCursor(Cursor.Type.Normal); 
	}
    }
}
