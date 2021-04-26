using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
	public GameObject rocketMenu;
	public GameObject launchResMenu;
	public void Enact(ButtonContext ctx)
	{
		if (ctx.metaManager.totalBudget < ctx.rocketManager.GetCost()) return;
		ctx.rocketManager.Launch();
		ctx.missionOutcomeManager.DoMission();
		launchResMenu.SetActive(true);
		rocketMenu.SetActive(false);
	}
}
