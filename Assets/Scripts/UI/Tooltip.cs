using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using Colony.Resources;
using Colony.Input;

namespace Colony.UI {

// Attach this component to a UI element to assign it a tooltip.
// If the tooltip text has the form {rSomething}, the resulting
// text will be the _resources_ needed to create Something, as defined
// in Costs.Get(Something).
public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public string Txt;

	private Text ttText;
	private GameObject ttPanel;

	void Start() {
		Debug.Assert(UIController.Instance.TooltipPanel != null, "Tooltip Panel is null!");
		Regex rgx = new Regex(@"{\w+}", RegexOptions.IgnoreCase);
		Txt = rgx.Replace(Txt, new MatchEvaluator(parseSpecial));
		ttText = UIController.Instance.TooltipPanel.GetComponentInChildren<Text>();
		ttPanel = UIController.Instance.TooltipPanel.transform.FindChild("Panel").gameObject;
		Debug.Assert(ttText != null && ttPanel != null, "ttText or ttPanel are null!");
	}

	void OnDisable() {
		hide();
	}

	public void OnPointerEnter(PointerEventData data) {
		var tt = UIController.Instance.TooltipPanel;
		ttPanel.SetActive(true);
		tt.transform.position = transform.position;
		ttText.text = Txt;
		// Prevent overflowing from screen
		var transf = tt.transform.position;
		float diffx = Screen.width - ttText.rectTransform.rect.width,
		      diffy = Screen.height  - ttText.rectTransform.rect.height;
		float newx = transf.x >= diffx ? diffx : transf.x,
		      newy = transf.y >= diffy ? diffy : transf.y;
		tt.transform.position = new Vector3(newx, newy, transf.z);
	}

	public void OnPointerExit(PointerEventData data) {
		hide();
	}

	private void hide() {
		if (ttPanel != null) 
			ttPanel.SetActive(false);
		if (ttText != null)
			ttText.text = "";
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
