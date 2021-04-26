using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[Serializable]
public class MissionListEntry
{
	public string id;
	public List<MissionBase> missions;
}

public class ProjectManager : MonoBehaviour
{
	// ----------------------------- Selection stuff -----------------------------
	public GameObject nameField;
	public GameObject rewardField;
	public GameObject difficultyField;

	public List<MissionListEntry> planets;
	int index = 0;
	MissionListEntry selected;
	// ---------------------------------------------------------------------------

	// ----------------------------- Post-Select stuff ---------------------------
	public MissionBase activeMission;
	public string projName;
	public int state = 0;
	// 0 - unlaunched
	// 1 - success
	// 2 - failure
	// ---------------------------------------------------------------------------

	private void Update()
	{
		if (selected != null)
		{
			if (selected.missions.Count > 0)
			{
				if (gameObject.GetComponent<RocketManager>().HasFlag(selected.missions[index].prereqMission)
				|| selected.missions[index].prereqMission.Length == 0)
				{
					if (gameObject.GetComponent<RocketManager>().HasFlag(selected.missions[index].id)
				&& !selected.missions[index].repeatable)
					{
						nameField.GetComponent<TextMeshPro>().text = selected.missions[index].name;
						rewardField.GetComponent<TextMeshPro>().text = "<color=red>You have already completed</color>";
						difficultyField.GetComponent<TextMeshPro>().text = "<color=red>this project.</color>";
					}
					else
					{
						nameField.GetComponent<TextMeshPro>().text = selected.missions[index].name;
						rewardField.GetComponent<TextMeshPro>().text = "Rewards: " + selected.missions[index].getRewards();
						difficultyField.GetComponent<TextMeshPro>().text = "Difficulty: " + selected.missions[index].getDifficulty();

					}
				}
				else
				{
					nameField.GetComponent<TextMeshPro>().text = selected.missions[index].name;
					rewardField.GetComponent<TextMeshPro>().text = "<color=red>You do not have the means to</color>";
					difficultyField.GetComponent<TextMeshPro>().text = "<color=red>successfully do this project right now.</color>";
				}
			}
			else
			{
				nameField.GetComponent<TextMeshPro>().text = "This";
				rewardField.GetComponent<TextMeshPro>().text = "Should Not";
				difficultyField.GetComponent<TextMeshPro>().text = "Appear";
			}
		}
		else
		{
			nameField.GetComponent<TextMeshPro>().text = "Select a Planet";
			rewardField.GetComponent<TextMeshPro>().text = "Each planet offers its own projects.";
			difficultyField.GetComponent<TextMeshPro>().text = "Closer planets involve less risk.";
		}
	}

	public void Select(string id)
	{
		Debug.Log("Planet were selecter: " + id);
		MissionListEntry entry = planets.Find(x => x.id == id);
		if (entry != null)
		{
			selected = entry;
			index = 0;
		}
		else
		{
			Debug.LogError("Something tried to select planet id " + id + " which does not exist in the Mission List Database.");
		}
	}

	public void DeSelect(bool ignore, bool isplan)
	{
		if (selected == null || (selected != null && !isplan)) return;
		selected = null;
	}

	public void Next()
	{
		if (selected == null || selected.missions.Count == 0) return;
		index = (index + 1) % selected.missions.Count;
	}

	public void Previous()
	{
		if (selected == null) return;
		index = index - 1 < 0 ? selected.missions.Count - 1 : index - 1;
	}

	public bool HasSelected()
	{
		return selected != null && (gameObject.GetComponent<RocketManager>().HasFlag(selected.missions[index].prereqMission) || selected.missions[index].prereqMission.Length == 0);
	}

	public MissionBase GetSelected()
	{
		return selected.missions[index];
	}
}
