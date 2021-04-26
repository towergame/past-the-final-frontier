using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoMainMenu : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject missionMenu;

	public void Enact(ButtonContext ctx)
	{
		//ctx.pman.DeSelect(true, true);
		ctx.clickHandler.ForceDeselect();
		mainMenu.SetActive(true);
		missionMenu.SetActive(false);
	}
}
