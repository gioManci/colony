using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using Colony.Resources;
using Colony.Input;

namespace Colony.UI.Tooltips {

// Attach this component to a UI element to assign it a tooltip.
// If the tooltip text has the form {rSomething}, the resulting
// text will be the _resources_ needed to create Something, as defined
// in Costs.Get(Something).
public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public string Txt;

	void Start() {
		Debug.Assert(UIController.Instance.tooltipText != null, "Tooltip Panel is null!");
		Regex rgx = new Regex(@"{\w+}", RegexOptions.IgnoreCase);
		Txt = rgx.Replace(Txt, new MatchEvaluator(parseSpecial));
	}

	public void OnPointerEnter(PointerEventData data) {
		var tt = UIController.Instance.tooltipText;
		tt.SetActive(true);
		tt.transform.position = transform.position;
		tt.GetComponent<Text>().text = Txt;
		// Prevent overflowing from screen
		var transf = tt.transform.position;
		var diff = Screen.width - tt.GetComponent<Text>().rectTransform.rect.width;
		if (transf.x >= diff)
			tt.transform.position = new Vector3(diff, transf.y, transf.z);
	}

	public void OnPointerExit(PointerEventData data) {
		UIController.Instance.tooltipText.SetActive(false);
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
