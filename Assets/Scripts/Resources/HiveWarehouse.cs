using UnityEngine;
using System.Collections;
using Colony.Hive;

namespace Colony.Resources
{
using Hive = Colony.Hive.Hive;
    public class HiveWarehouse : MonoBehaviour
    {
	// Rate at which resources are converted by cells used for refining
	private const float REFINE_INTERVAL = 8f; // seconds

	// Conversion rate for resource refining
	public int PollenForHoney;
	public int PollenForRoyalJelly;
	public int WaterForRoyalJelly;
	public int RefinedHoneyYield;
	public int RefinedRoyalJellyYield;

        private ResourceManager rm;
	private Hive hive;
	private float refineTimer = REFINE_INTERVAL;

        public bool IsEmpty { get { return rm.IsEmpty; } }

        void Start()
        {
            rm = FindObjectOfType<ResourceManager>();
            hive = gameObject.GetComponent<Hive>();
            Debug.Assert(hive != null, "Hive is null for this HiveWarehouse!");
        }
		
	void Update() {
		// Refine resources
		refineTimer -= Time.deltaTime;
		if (refineTimer <= 0) {
			refineTimer = REFINE_INTERVAL;
			foreach (GameObject cell in hive.Cells) {
				var c = cell.GetComponent<Cell>();
				if (c.CellState == Cell.State.Refine) {
					refineResources(c.Refined);
				}
			}
		}
	}

        public void AddResources(ResourceSet resources)
        {
            rm.AddResources(resources);
        }

        public void RemoveResources(ResourceSet resources)
        {
            rm.RemoveResources(resources);
        }

	// Convert XXX Pollen (should be Nectar, but unimplemented) to YYY Honey
	// and WWW Water + ZZZ Pollen to RoyalJelly
	private void refineResources(Cell.RefinedResource what) {
		// Ensure the player has all the due resources.

		bool refiningRJ = (what & Cell.RefinedResource.RoyalJelly) != 0,
		     refiningHoney = (what & Cell.RefinedResource.Honey) != 0;

		if (refiningHoney && rm.GetResource(ResourceType.Water) >= WaterForRoyalJelly) {
			rm.RemoveResource(ResourceType.Pollen, PollenForHoney);
			rm.AddResource(ResourceType.Honey, RefinedHoneyYield);
		}

		if (refiningRJ && rm.GetResource(ResourceType.Pollen) >= PollenForHoney + PollenForRoyalJelly) {
			rm.RemoveResource(ResourceType.Pollen, PollenForRoyalJelly);
			rm.RemoveResource(ResourceType.Water, WaterForRoyalJelly);
			rm.AddResource(ResourceType.RoyalJelly, RefinedRoyalJellyYield);
		}
	}
    }
}