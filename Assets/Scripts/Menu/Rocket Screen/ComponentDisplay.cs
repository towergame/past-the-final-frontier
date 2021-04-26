using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using TMPro;

public class ComponentDisplay : MonoBehaviour
{
	public GameObject title;
	public GameObject desc;

	[ReadOnly]
	public ComponentBase component;

	// Update is called once per frame
	void Update()
	{
		if (component != null)
		{
			TextMeshPro titleTMP = title.GetComponent<TextMeshPro>();
			titleTMP.text = component.modelName;

			TextMeshPro descTMP = desc.GetComponent<TextMeshPro>();
			string res = "";
			res += "Cost: " + component.cost + "M\n";
			res += "Tier: " + getTier(component.tier);
			descTMP.text = res;
		}
	}

	string getTier(int tier)
	{
		switch (tier)
		{
			case 1:
				return "<color=green>I</color>";
			case 2:
				return "<color=yellow>II</color>";
			case 3:
				return "<color=orange>III</color>";
			case 4:
				return "<color=red>IV</color>";
			case 5:
				return "<color=purple>V</color>";
			default:
				return "<color=white>?</color>";
		}
	}
}
