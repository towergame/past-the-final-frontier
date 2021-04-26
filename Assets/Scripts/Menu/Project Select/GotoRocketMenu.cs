using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GotoRocketMenu : MonoBehaviour
{
	public GameObject input;

	public GameObject rocketMenu;
	public GameObject projectSelectionMenu;
	public void Enact(ButtonContext ctx)
	{
		if (!ctx.pman.HasSelected()) return;
		if (ctx.rocketManager.HasFlag(ctx.pman.GetSelected().id) && !ctx.pman.GetSelected().repeatable) return;
		string output = input.GetComponent<TMP_InputField>().text;
		ctx.pman.projName = output;
		ctx.pman.activeMission = ctx.pman.GetSelected();
		ctx.clickHandler.ForceDeselect();

		//Debug.Log("Fuck you");
		rocketMenu.SetActive(true);
		projectSelectionMenu.SetActive(false);
	}
}
