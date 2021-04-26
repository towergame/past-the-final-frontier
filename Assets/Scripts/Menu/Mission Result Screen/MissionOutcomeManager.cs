using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionOutcomeManager : MonoBehaviour
{
	public GameObject outcomeObject;
	public GameObject pmObject;
	public GameObject rewardObject;

	public void DoMission()
	{
		string outcome = "Post Mortem: ";

		RocketManager rocketManager = gameObject.GetComponent<RocketManager>();
		bool epic = rocketManager.DetermineSuccess();

		if (rocketManager.ForceExceeds())
		{
			outcomeObject.GetComponent<TextMeshPro>().text = "Mission <color=red>Failure</color>";
			outcome += "Rocket failed to generate enough force during take-off.";
			ApplyFailRewards();
		}
		else if (rocketManager.FuelExceeds())
		{
			outcomeObject.GetComponent<TextMeshPro>().text = "Mission <color=red>Failure</color>";
			outcome += "Rockets ran out of fuel during its exit from the Earth's atmosphere.";
			ApplyFailRewards();
		}
		else if (epic)
		{
			outcomeObject.GetComponent<TextMeshPro>().text = "Mission <color=green>Success</color>";
			outcome += gameObject.GetComponent<ProjectManager>().activeMission.successMessage;
			ApplySuccessRewards();
		}
		else if (!epic) // wtf rocket not epic?? CRINGE!!!
		{
			outcomeObject.GetComponent<TextMeshPro>().text = "Mission <color=red>Failure</color>";
			outcome += "Rocket suffered critical failure during subspace flight.";
			ApplyFailRewards();
		}
		else
		{
			outcomeObject.GetComponent<TextMeshPro>().text = "Mission <color=yellow>???</color>";
			outcome += "amogus";
			//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣤⣤⣤⣀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀
			// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⠟⠉⠉⠉⠉⠉⠉⠉⠙⠻⢶⣄⠀⠀⠀⠀⠀
			// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣷⡀⠀⠀⠀
			// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⡟⠀⣠⣶⠛⠛⠛⠛⠛⠛⠳⣦⡀⠀⠘⣿⡄⠀⠀
			// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⠁⠀⢹⣿⣦⣀⣀⣀⣀⣀⣠⣼⡇⠀⠀⠸⣷⠀⠀
			// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⡏⠀⠀⠀⠉⠛⠿⠿⠿⠿⠛⠋⠁⠀⠀⠀⠀⣿⡄⣠
			// ⠀⠀⢀⣀⣀⣀⠀⠀⢠⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⡇⠀
			// ⠿⠿⠟⠛⠛⠉⠀⠀⣸⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀
			// ⠀⠀⠀⠀⠀⠀⠀⠀⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣧⠀
			// ⠀⠀⠀⠀⠀⠀⠀⢸⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⠀
			// ⠀⠀⠀⠀⠀⠀⠀⣾⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀
			// ⠀⠀⠀⠀⠀⠀⠀⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀
			// ⠀⠀⠀⠀⠀⠀⢰⣿⠀⠀⠀⠀⣠⡶⠶⠿⠿⠿⠿⢷⣦⠀⠀⠀⠀⠀⠀⠀⣿⠀
			// ⠀⠀⣀⣀⣀⠀⣸⡇⠀⠀⠀⠀⣿⡀⠀⠀⠀⠀⠀⠀⣿⡇⠀⠀⠀⠀⠀⠀⣿⠀
			// ⣠⡿⠛⠛⠛⠛⠻⠀⠀⠀⠀⠀⢸⣇⠀⠀⠀⠀⠀⠀⣿⠇⠀⠀⠀⠀⠀⠀⣿⠀
			// ⢻⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⡟⠀⠀⢀⣤⣤⣴⣿⠀⠀⠀⠀⠀⠀⠀⣿⠀
			// ⠈⠙⢷⣶⣦⣤⣤⣤⣴⣶⣾⠿⠛⠁⢀⣶⡟⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡟⠀
			// ⢷⣶⣤⣀⠉⠉⠉⠉⠉⠄⠀⠀⠀⠀⠈⣿⣆⡀⠀⠀⠀⠀⠀⠀⢀⣠⣴⡾⠃⠀
			// ⠀⠈⠉⠛⠿⣶⣦⣄⣀⠀⠀⠀⠀⠀⠀⠈⠛⠻⢿⣿⣾⣿⡿⠿⠟⠋⠁⠀
			// funny certification
		}

		pmObject.GetComponent<TextMeshPro>().text = outcome;
	}

	int dataFailReward = 20;
	int budgetFailReward = -5;
	int prFailReward = -3;
	public void ApplyFailRewards()
	{
		string rewards = "Rewards: \n";
		MetaManager metaManager = gameObject.GetComponent<MetaManager>();

		int dataR = (int)Math.Round((1 + UnityEngine.Random.Range(-0.25f, 0.25f)) * dataFailReward);
		metaManager.currData += dataR;
		rewards += $"- {dataR} units of <color=#00ffff>Data</color>\n";

		int budgetR = (int)Math.Round((1 + UnityEngine.Random.Range(-0.25f, 0.25f)) * budgetFailReward);
		metaManager.totalBudget += budgetR;
		rewards += $"- A {Math.Abs(budgetR)}M <color=red>decrease</color> in <color=yellow>Budget</color>\n";

		int prR = (int)Math.Round((1 + UnityEngine.Random.Range(-0.5f, 0.5f)) * prFailReward);
		metaManager.currentStanding += prR;
		rewards += $"- Significantly worsened <color=green>Public Image</color>";

		rewardObject.GetComponent<TextMeshPro>().text = rewards;
		gameObject.GetComponent<ProjectManager>().state = 2;
	}

	public void ApplySuccessRewards()
	{
		string rewards = "Rewards: \n";
		MetaManager metaManager = gameObject.GetComponent<MetaManager>();
		ProjectManager projectManager = gameObject.GetComponent<ProjectManager>();

		int dataWinReward = projectManager.activeMission.rewards.meanData;
		int budgetWinReward = projectManager.activeMission.rewards.meanBudget;
		int prWinReward = projectManager.activeMission.rewards.meanPR;

		if (dataWinReward != 0)
		{
			int dataR = (int)Math.Round((1 + UnityEngine.Random.Range(-0.25f, 0.25f)) * dataFailReward);
			metaManager.currData += dataR;
			rewards += $"- {dataR} units of <color=#00ffff>Data</color>\n";
		}
		if (budgetWinReward != 0)
		{
			int budgetR = (int)Math.Round((1 + UnityEngine.Random.Range(-0.25f, 0.25f)) * budgetWinReward);
			metaManager.totalBudget += budgetR;
			if (budgetR > 0) rewards += $"- A {Math.Abs(budgetR)}M <color=green>increase</color> in <color=yellow>Budget</color>\n";
			else if (budgetR < 0) rewards += $"- A {Math.Abs(budgetR)}M <color=red>decrease</color> in <color=yellow>Budget</color>\n";
		}
		if (budgetWinReward != 0)
		{
			int prR = (int)Math.Round((1 + UnityEngine.Random.Range(-0.5f, 0.5f)) * prWinReward);
			metaManager.currentStanding += prR;
			if (prR > 0) rewards += $"- Significantly improved <color=green>Public Image</color>";
			else if (prR < 0) rewards += $"- Significantly worsened <color=green>Public Image</color>";
		}

		rewardObject.GetComponent<TextMeshPro>().text = rewards;

		gameObject.GetComponent<RocketManager>().AddFlag(projectManager.activeMission.id);
		projectManager.state = 1;
	}
}
