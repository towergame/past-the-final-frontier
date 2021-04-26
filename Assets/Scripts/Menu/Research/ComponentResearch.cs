using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComponentResearch : MonoBehaviour
{
	public GameObject manager;
	public string componentId;

	public GameObject desc;

	void Update()
	{
		int tier = manager.GetComponent<MetaManager>().GetTier(componentId);

		string description = "Current Tier: " + getTier(tier) + "\nUpgrade Cost: " + manager.GetComponent<MetaManager>().getUpgradeCost(componentId);

		desc.GetComponent<TextMeshPro>().text = description;
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
				return $"<color=white>{tier}</color>";
		}
	}
}
