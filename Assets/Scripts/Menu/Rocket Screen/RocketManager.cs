using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class ComponentEntry
{
	public string id;
	public int currIndex;
	public List<ComponentBase> components;
}

public class RocketManager : MonoBehaviour
{

	public ComponentDisplay cockpitDisp;
	public ComponentDisplay boosterDisp;
	public ComponentDisplay fuelDisp;
	public ComponentDisplay wingDisp;
	public ComponentDisplay engineDisp;

	public GameObject summary;
	public GameObject title;


	public List<ComponentEntry> components;

	HashSet<string> flags;

	int currBudgetUsage = 50;
	float weight = 0;
	float force = 0;
	float forceMod = 1;
	int fuel = 0;
	float dist = 10;
	float eta = 10; // Roughly 1 cycle per 0.825 AUs

	bool launched = false;


	private void Start()
	{
		flags = new HashSet<string>();
	}

	private void Update()
	{
		if (!launched)
		{
			currBudgetUsage = 50;
			weight = 0;
			force = 0;
			forceMod = 1;
			fuel = 0;
			dist = gameObject.GetComponent<ProjectManager>().activeMission != null ? gameObject.GetComponent<ProjectManager>().activeMission.distance / 150000000 : 10;
			eta = (float)Math.Ceiling(dist / 0.825f);

			weight += gameObject.GetComponent<ProjectManager>().activeMission != null ? gameObject.GetComponent<ProjectManager>().activeMission.payloadWeight : 0;

			ComponentEntry cockEntry = components.Find(x => x.id == "cockpit");
			cockpitDisp.component = cockEntry.components[cockEntry.currIndex];
			currBudgetUsage += cockEntry.components[cockEntry.currIndex].cost;
			weight += cockEntry.components[cockEntry.currIndex].additionalWeight;

			ComponentEntry bootEntry = components.Find(x => x.id == "booster");
			boosterDisp.component = bootEntry.components[bootEntry.currIndex];
			currBudgetUsage += bootEntry.components[bootEntry.currIndex].cost;
			weight += bootEntry.components[bootEntry.currIndex].additionalWeight;

			ComponentEntry fuelEntry = components.Find(x => x.id == "fueltank");
			fuelDisp.component = fuelEntry.components[fuelEntry.currIndex];
			currBudgetUsage += fuelEntry.components[fuelEntry.currIndex].cost;
			weight += fuelEntry.components[fuelEntry.currIndex].additionalWeight;
			fuel += fuelEntry.components[fuelEntry.currIndex].additionalFuel;

			ComponentEntry wingEntry = components.Find(x => x.id == "wings");
			wingDisp.component = wingEntry.components[wingEntry.currIndex];
			currBudgetUsage += wingEntry.components[wingEntry.currIndex].cost;
			weight += wingEntry.components[wingEntry.currIndex].additionalWeight;
			forceMod *= wingEntry.components[wingEntry.currIndex].forceSlash;

			ComponentEntry engineEntry = components.Find(x => x.id == "engine");
			engineDisp.component = engineEntry.components[engineEntry.currIndex];
			currBudgetUsage += engineEntry.components[engineEntry.currIndex].cost;
			weight += engineEntry.components[engineEntry.currIndex].additionalWeight;
			force += engineEntry.components[engineEntry.currIndex].additionalForce;
			forceMod *= engineEntry.components[engineEntry.currIndex].forceSlash;

			eta /= force / (float)Math.Round(weight * 9.8 * forceMod);

			string res = $@"Current using {currBudgetUsage}M out of a budget of {gameObject.GetComponent<MetaManager>().totalBudget}M RMR
Current Readiness: {GetPrediction()}
Weight: {weight} t
Liftoff Force: {force} kN (Required: {Math.Round(weight * 9.8 * forceMod)} kN)
Fuel: {fuel}L ({Math.Round(weight * 9.8 * forceMod * 5)}L required for lift-off)

Distance to Target: {dist} AUs";
			//ETA: {eta} cycles ({eta * 5}s)";
			summary.GetComponent<TextMeshPro>().text = res;
			string projn = gameObject.GetComponent<ProjectManager>().projName != null ? gameObject.GetComponent<ProjectManager>().projName : "Nil";
			title.GetComponent<TextMeshPro>().text = $"Project: \"{projn}\"";
		}
	}

	public int GetCost()
	{
		return currBudgetUsage;
	}

	public void Launch()
	{
		if (currBudgetUsage < gameObject.GetComponent<MetaManager>().totalBudget) // replace with total budget later
		{
			launched = true;
		}
	}

	public void UnLaunch()
	{
		launched = false;
	}

	public string GetPrediction()
	{
		if (fuel < weight * 9.8 * forceMod * 5 || force < weight * 9.8 * forceMod)
		{
			return "<color=red>It'll be a miracle if it even takes off</color>";
		}
		else if (fuel / (weight * 9.8 * forceMod * 5) > 2 && force / (weight * 9.8 * forceMod) > 2)
		{
			return "<color=green>Excellent</color>";
		}
		else
		{
			return "<color=yellow>Adequate</color>";
		}
	}

	public void AddFlag(string flag)
	{
		flags.Add(flag);
	}

	public void ClearFlag(string flag)
	{
		flags.Remove(flag);
	}

	public bool HasFlag(string flag)
	{
		return flags.Contains(flag);
	}

	public void NextT(string comp)
	{
		MetaManager metaManager = gameObject.GetComponent<MetaManager>();
		int index = components.FindIndex(x => x.id == comp);
		do
		{
			components[index].currIndex = (components[index].currIndex + 1) % components[index].components.Count;
		} while (!components[index].components[components[index].currIndex].requiredFlags.TrueForAll(x => flags.Contains(x)) || components[index].components[components[index].currIndex].tier > metaManager.GetTier(comp));
	}

	public void LastT(string comp)
	{
		MetaManager metaManager = gameObject.GetComponent<MetaManager>();
		int index = components.FindIndex(x => x.id == comp);
		do
		{
			components[index].currIndex = components[index].currIndex - 1 < 0 ? components[index].components.Count - 1 : components[index].currIndex - 1;
		} while (!components[index].components[components[index].currIndex].requiredFlags.TrueForAll(x => flags.Contains(x)) || components[index].components[components[index].currIndex].tier > metaManager.GetTier(comp));
	}

	public bool ForceExceeds()
	{
		int miracleMod = UnityEngine.Random.Range(0, 100000) == 42069 ? 9999 : 0; // 1 in a hundred thousand, I like my chances :)
		return force + miracleMod < Math.Round(weight * 9.8 * forceMod);
	}

	public bool FuelExceeds()
	{
		int miracleMod = UnityEngine.Random.Range(0, 100000) == 42069 ? 9999 : 0;
		return fuel + miracleMod < Math.Round(weight * 9.8 * forceMod * 5);
	}

	public bool DetermineSuccess()
	{
		float mod = (fuel / (weight * 9.8f * forceMod * 5) + force / (weight * 9.8f * forceMod)) / eta; // The longer a rocket is in space the higher of chance for it to get fucked. I am a rocket scientist.
		return UnityEngine.Random.Range(0, 100000) > 50000 / mod; // Is my core game mechanic literally a glorified coin flip? Yes. Cope.
	}

	public void LoadFlags()
	{
		string raw = PlayerPrefs.GetString("flags", "");
		raw.Split('|').ToList().ForEach(x => flags.Add(x));
		Debug.Log(raw);
	}

	public void SaveFlags()
	{
		string raw = "";
		flags.ToList().ForEach(x => raw += x + "|");
		raw = raw.Substring(0, raw.Length - 1);
		Debug.Log(raw);
		PlayerPrefs.SetString("flags", raw);
	}
}
