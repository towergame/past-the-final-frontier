using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToMainMenu : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject launchMenu;
	public GameObject restartMenu;

	public GameObject txtField;

	string failMessage = $@"The Comissariat wishes to remind you that, while understandable, failures are not tolerated and will be put under heavy scrutiny. May this be the last failure of this program. Your performance is being monitored and evaluated.";

	public void Enact(ButtonContext ctx)
	{
		switch (ctx.pman.state)
		{
			case 1:
				txtField.GetComponent<TextMeshPro>().text = ctx.pman.activeMission.successMail;
				break;
			case 2:
				txtField.GetComponent<TextMeshPro>().text = failMessage;
				break;
		}
		ctx.pman.projName = "";
		ctx.pman.activeMission = null;
		ctx.rocketManager.UnLaunch();
		ctx.metaManager.Save();

		if ((ctx.metaManager.totalBudget < 550 || ctx.metaManager.currentStanding < -25) && restartMenu != null)
		{
			ctx.metaManager.EndGame(0);
			restartMenu.SetActive(true);
			launchMenu.SetActive(false);
			return;
		}
		if (ctx.rocketManager.HasFlag("pdexplore") && restartMenu != null)
		{
			ctx.metaManager.EndGame(1);
			restartMenu.SetActive(true);
			launchMenu.SetActive(false);
			return;
		}
		if (ctx.rocketManager.HasFlag("sswarm") && restartMenu != null)
		{
			ctx.metaManager.EndGame(2);
			restartMenu.SetActive(true);
			launchMenu.SetActive(false);
			return;
		}

		// Change to le rocket scree
		mainMenu.SetActive(true);
		launchMenu.SetActive(false);
	}
}
