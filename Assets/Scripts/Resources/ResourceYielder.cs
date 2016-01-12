using UnityEngine;
using System;
using Colony.UI;

namespace Colony.Resources
{
    [RequireComponent(typeof(Selectable))]
    public class ResourceYielder : MonoBehaviour
    {
        public int initialPollen;
        public int initialNectar;
        public int initialWater;
        public int initialHoney;
        public int initialRoyalJelly;
        public int initialBeeswax;
        public int defaultPollenYield;
        public int defaultNectarYield;
        public int defaultWaterYield;
        public int defaultHoneyYield;
        public int defaultRoyalJellyYield;
        public int defaultBeeswaxYield;

	public ResourceSet Resources { get; private set; }

        public bool IsDepleted { get { return Resources.IsEmpty(); } }

        void Start()
        {
            Resources = new ResourceSet()
                .With(ResourceType.Beeswax, initialBeeswax)
                .With(ResourceType.Honey, initialHoney)
                .With(ResourceType.Nectar, initialNectar)
                .With(ResourceType.Pollen, initialPollen)
                .With(ResourceType.RoyalJelly, initialRoyalJelly)
                .With(ResourceType.Water, initialWater);

		var sel = GetComponent<Selectable>();
		sel.OnSelect += () => {
			UIController.Instance.SetResourceBPText(this);
			UIController.Instance.SetBottomPanel(UIController.BPType.Text);
		};
		sel.OnDeselect += () => UIController.Instance.SetBottomPanel(UIController.BPType.None);
        }

        public ResourceSet Yield(ResourceSet request)
        {
            ResourceSet result = new ResourceSet();
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                result[type] = Mathf.Min(request[type], Resources[type]);
            }
            Resources -= request;

            if (Resources.IsEmpty())
            {
                gameObject.SetActive(false);
            }
            else if (GetComponent<Selectable>().IsSelected)
            {
                UIController.Instance.SetResourceBPText(this);
            }

            return result;
        }

	public int YieldAmount(ResourceType type) {
		switch (type) {
		case ResourceType.Beeswax:
			return defaultBeeswaxYield;
		case ResourceType.Honey:
			return defaultHoneyYield;
		case ResourceType.Nectar:
			return defaultNectarYield;
		case ResourceType.Pollen:
			return defaultPollenYield;
		case ResourceType.RoyalJelly:
			return defaultRoyalJellyYield;
		case ResourceType.Water:
			return defaultWaterYield;
		}
		return 0;
	}
    }
}
