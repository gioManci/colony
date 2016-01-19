using UnityEngine;
using System.Collections;
using Colony.Hive;

namespace Colony.Resources
{

using Hive = Colony.Hive.Hive;

    public class HiveWarehouse : MonoBehaviour
    {
	// Rate at which resources are converted by cells used for refining
	public float RefineInterval = 6f; // seconds
	public float InkeeperMultiplier = 2f;

	// Conversion rate for resource refining
	public int PollenForHoney = 4;
	public int WaterForHoney = 4;	
	public int PollenForRoyalJelly = 5;
	public int WaterForRoyalJelly = 5;
	public int RefinedHoneyYield = 2;
	public int RefinedRoyalJellyYield = 1;

        private ResourceManager rm;
	private Hive hive;
	private float refineTimer;

        public bool IsEmpty { get { return rm.IsEmpty; } }

        void Start()
        {
            rm = FindObjectOfType<ResourceManager>();
            hive = gameObject.GetComponent<Hive>();
            Debug.Assert(hive != null, "Hive is null for this HiveWarehouse!");
            refineTimer = RefineInterval;
        }
		
	void Update() {
		// Refine resources
		refineTimer -= Time.deltaTime;
		if (refineTimer <= 0) {
			refineTimer = RefineInterval;
			foreach (GameObject cell in hive.Cells) {
				var c = cell.GetComponent<Cell>();
				if (c.CellState == Cell.State.Refine) {
					if (c.Inkeeper == null)
						rm.AddResources(refineResources(c.Refined));
					else
						rm.AddResources(refineResources(c.Refined) * InkeeperMultiplier);
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
	private ResourceSet refineResources(Cell.RefinedResource what) {
		var res = new ResourceSet();

		// Ensure the player has all the due resources.

		bool refiningRJ = (what & Cell.RefinedResource.RoyalJelly) != 0,
		     refiningHoney = (what & Cell.RefinedResource.Honey) != 0;

		int pollen = rm.GetResource(ResourceType.Pollen),
		    water = rm.GetResource(ResourceType.Water);

		if (refiningHoney && pollen >= PollenForHoney && water >= WaterForHoney) {
			rm.RemoveResource(ResourceType.Pollen, PollenForHoney);
			rm.RemoveResource(ResourceType.Water, WaterForHoney);
			res += new ResourceSet().With(ResourceType.Honey, RefinedHoneyYield);
		}

		if (refiningRJ && pollen >= PollenForRoyalJelly && water >= WaterForRoyalJelly) {
			rm.RemoveResource(ResourceType.Pollen, PollenForRoyalJelly);
			rm.RemoveResource(ResourceType.Water, WaterForRoyalJelly);
			res += new ResourceSet().With(ResourceType.RoyalJelly, RefinedRoyalJellyYield);
		}

		return res;
	}
    }
}