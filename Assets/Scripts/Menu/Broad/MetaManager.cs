using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MetaManager : MonoBehaviour
{
	public int totalBudget;
	public float currentStanding; // If this dips under -25, you get executed.
	public int currData;
	public int cResTier;
	public int bResTier;
	public int fResTier;
	public int wResTier;
	public int eResTier;

	public GameObject basicInfo;
	public GameObject dataCounter;

	public AudioSource click;
	public AudioClip clicko;
	public AudioSource music;
	public AudioSource hum;

	public Slider sfxs;
	public Slider ms;

	public TextMeshProUGUI end;

	private void Start()
	{
		totalBudget = 600;
		currentStanding = 0;
		currData = 0;
		cResTier = 1;
		bResTier = 1;
		fResTier = 1;
		wResTier = 1;
		eResTier = 1;
		Load();
		LoadSFX();
		LoadMusic();
	}

	private void Load()
	{
		totalBudget = PlayerPrefs.GetInt("totalBudget", totalBudget);
		currentStanding = PlayerPrefs.GetFloat("currentStanding", currentStanding);
		currData = PlayerPrefs.GetInt("currData", currData);
		cResTier = PlayerPrefs.GetInt("cResTier", cResTier);
		bResTier = PlayerPrefs.GetInt("bResTier", bResTier);
		fResTier = PlayerPrefs.GetInt("fResTier", fResTier);
		wResTier = PlayerPrefs.GetInt("wResTier", wResTier);
		eResTier = PlayerPrefs.GetInt("eResTier", eResTier);

		gameObject.GetComponent<RocketManager>().LoadFlags();
	}

	public void Save()
	{
		PlayerPrefs.SetInt("totalBudget", totalBudget);
		PlayerPrefs.SetFloat("currentStanding", currentStanding);
		PlayerPrefs.SetInt("currData", currData);
		PlayerPrefs.SetInt("cResTier", cResTier);
		PlayerPrefs.SetInt("bResTier", bResTier);
		PlayerPrefs.SetInt("fResTier", fResTier);
		PlayerPrefs.SetInt("wResTier", wResTier);
		PlayerPrefs.SetInt("eResTier", eResTier);

		gameObject.GetComponent<RocketManager>().SaveFlags();
	}

	private void Update()
	{
		string res = $@"<color=#00FFFF>Data</color>: {currData} units
<color=yellow>Budget</color>: {totalBudget}M
<color=green>Public Opinion</color>: {GetStanding()}";

		basicInfo.GetComponent<TextMeshPro>().text = res;
		dataCounter.GetComponent<TextMeshPro>().text = $"You currently have {currData} units of <color=#00FFFF>Data</color>";
	}

	string GetStanding()
	{
		if (currentStanding < -10)
		{
			return "<color=red>Abysmal</color>";
		}
		else if (currentStanding < 10)
		{
			return "<color=yellow>Neutral</color>";
		}
		else
		{
			return "<color=green>Good</color>";
		}
	}

	public int GetTier(string component)
	{
		switch (component)
		{
			case "cockpit":
				return cResTier;
			case "booster":
				return bResTier;
			case "fueltank":
				return fResTier;
			case "wings":
				return wResTier;
			case "engine":
				return eResTier;
			default:
				Debug.LogError("Unknown component id: " + component);
				return 69;
		}
	}

	public void AddTier(string component)
	{
		switch (component)
		{
			case "cockpit":
				cResTier++;
				return;
			case "booster":
				bResTier++;
				return;
			case "fueltank":
				fResTier++;
				return;
			case "wings":
				wResTier++;
				return;
			case "engine":
				eResTier++;
				return;
			default:
				Debug.LogError("Unknown component id: " + component);
				return;
		}
	}

	public void Upgrade(string component)
	{
		int cost = getUpgradeCost(component);
		if (currData >= cost)
		{
			currData -= cost;
			AddTier(component);
			Save();
		}
	}

	public int getUpgradeCost(string component)
	{
		int tier = GetTier(component);
		return (int)Math.Pow(2, tier) * 10;
	}

	public void playClick()
	{
		click.PlayOneShot(clicko);
	}

	public void ChangeSFX(float v)
	{
		click.volume = v;
		hum.volume = v;

		PlayerPrefs.SetFloat("sfxV", v);
	}

	public void ChangeMusic(float v)
	{
		music.volume = v;

		PlayerPrefs.SetFloat("mV", v);
	}

	public void LoadSFX()
	{
		click.volume = PlayerPrefs.GetFloat("sfxV", 1);
		hum.volume = PlayerPrefs.GetFloat("sfxV", 1);
		sfxs.value = click.volume;
	}

	public void LoadMusic()
	{
		music.volume = PlayerPrefs.GetFloat("mV", 0.5f);
		ms.value = music.volume;
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void Restart()
	{
		totalBudget = 600;
		currentStanding = 0;
		currData = 0;
		cResTier = 1;
		bResTier = 1;
		fResTier = 1;
		wResTier = 1;
		eResTier = 1;
		Save();
		PlayerPrefs.SetString("flags", "");
		Load();
	}

	public void EndGame(int endId)
	{
		string mismanage = @"[Notice of Termination]
After reviewing your recent performance, the Comissariat has decided to deem you unfit for leadership.

You are to vacate the premises within 2 days. A different job will be provided to you.";
		string disobey = @"[Notice of Termination]
Due to refusal to follow direct orders set out by the Comissariat, you have been sentenced with treason.

The punishment is death.
Your belongings will be vacated from the premises within 2 days.";

		string dyson = @"[Notice of Termination]
Following the inmeasurable progress achieved by your leadership of the RSRA, the Comissariat has decided to award with the Order of the Highest Honour. 

Additionally, the Comissariat has decided to permit you to enter early retirement. Please vacate your belongings from the premises within 2 days.";

		switch (endId)
		{
			case 0:
				end.text = mismanage;
				break;
			case 1:
				end.text = disobey;
				break;
			case 2:
				end.text = dyson;
				break;
			default:
				end.text = "Sussus Amogus";
				break;
		}
	}
}
