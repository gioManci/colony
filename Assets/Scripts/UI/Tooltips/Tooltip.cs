using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using Colony.Resources;
using Colony.Input;

namespace Colony.UI.Tooltips {

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public string Txt;
	
	private GameObject tooltipPanel;

	void Start() {
		tooltipPanel = GameObject.Find("TooltipText");
		Debug.Assert(tooltipPanel != null, "Tooltip Panel is null!");
		Regex rgx = new Regex(@"{\w+}", RegexOptions.IgnoreCase);
		Txt = rgx.Replace(Txt, new MatchEvaluator(parseSpecial));
	}

	public void OnPointerEnter(PointerEventData data) {
		tooltipPanel.SetActive(true);
		tooltipPanel.transform.position = gameObject.transform.position;
		tooltipPanel.GetComponent<Text>().text = Txt;
	}

	public void OnPointerExit(PointerEventData data) {
		tooltipPanel.SetActive(false);
	}

	private string parseSpecial(Match m) {
		string txt = m.ToString();
		if (txt.Length > 4) {
			switch (txt[1]) {
			case 'r':
				return Costs.Get(txt.Substring(2, txt.Length - 3)).ToString();
			}
		}
		return txt;
	}
}

}
