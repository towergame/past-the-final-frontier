using System;
using UnityEngine;

[Serializable]
public struct Rewards
{
	public int meanPR;
	public int meanData;
	public int meanBudget;
}

[Serializable]
public class MissionBase
{
	public string name;
	public string id;
	public Rewards rewards;
	public string successMessage;

	public string prereqMission;
	public bool repeatable;

	[TextArea]
	public string successMail;

	public long distance; // distance to location in km
	public float payloadWeight; // payload weight in tonnes
	float difficulty { get { return (distance / 150000000.0f) * ((payloadWeight / 1.025f)); } } // Difficulty is AUs x 10 until destination times payload weight in Perserverances

	public virtual string getRewards()
	{
		string res = "";
		if (rewards.meanPR > 0) res += "PR";
		if (res.Length > 0 && rewards.meanData > 0) res += "; ";
		if (rewards.meanData > 0) res += "Data";
		if (res.Length > 0 && rewards.meanBudget > 0) res += "; ";
		if (rewards.meanBudget > 0) res += "Budget";
		return res;
	}

	public virtual string getDifficulty()
	{
		string res = "";
		if (difficulty < 5)
		{
			res += "<color=green>Routine (" + Math.Floor(difficulty) + ")</color>";
		}
		else if (difficulty < 10)
		{
			res += "<color=yellow>Challenging (" + Math.Floor(difficulty) + ")</color>";
		}
		else if (difficulty < 20)
		{
			res += "<color=orange>Groundbreaking (" + Math.Floor(difficulty) + ")</color>";
		}
		else if (difficulty < 30)
		{
			res += "<color=red>Insane (" + Math.Floor(difficulty) + ")</color>";
		}
		else
		{
			res += "<color=purple>Deranged (" + Math.Floor(difficulty) + ")</color>";
		}

		return res;
	}
}
