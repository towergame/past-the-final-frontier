using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMission : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject missionScreen;

	public void StartMissione()
	{
		missionScreen.SetActive(true);
		mainMenu.SetActive(false);
	}
}
