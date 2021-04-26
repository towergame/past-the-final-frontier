using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct ButtonContext
{
	// Put all the manager references here
	public ProjectManager pman;
	public RocketManager rocketManager;
	public MissionOutcomeManager missionOutcomeManager;
	public MetaManager metaManager;
	public ClickHandler clickHandler;
}

public class ClickHandler : MonoBehaviour
{
	public UnityEvent<bool, bool> clicked; // This is mainly supposed to be used for de-select actions, but the boolean should be true if it's a left click.

	// Update is called once per frame
	// UpDaTe Is CaLlEd OnCe PeR fRaMe
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider[] stuff = Physics.OverlapCapsule(RandomUsefulScripts.Vec2ToVec3(mPos, -100), RandomUsefulScripts.Vec2ToVec3(mPos, 100), 0.1f);
			ButtonBase button = null;

			foreach (Collider x in stuff)
			{
				if (x.gameObject.TryGetComponent<ButtonBase>(out button))
				{
					break;
				}
			}

			if (button)
			{
				ButtonContext bcx = new ButtonContext();
				bcx.pman = gameObject.GetComponent<ProjectManager>();
				bcx.rocketManager = gameObject.GetComponent<RocketManager>();
				bcx.missionOutcomeManager = gameObject.GetComponent<MissionOutcomeManager>();
				bcx.metaManager = gameObject.GetComponent<MetaManager>();
				bcx.clickHandler = this;
				Planet discar;
				bool isPlanet = button.TryGetComponent<Planet>(out discar);
				clicked.Invoke(false, isPlanet); // This is absolute spaghetti. Cope.
				button.Click(bcx);
			}
		}
		else if (Input.GetMouseButton(1))
		{
			clicked.Invoke(true, true);
		}
	}

	public void ForceDeselect()
	{
		clicked.Invoke(true, true);
	}

	private void OnDrawGizmos()
	{
		Vector2 idek = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider[] stuff = Physics.OverlapCapsule(RandomUsefulScripts.Vec2ToVec3(idek, -100), RandomUsefulScripts.Vec2ToVec3(idek, 100), 0.1f);
		Gizmos.DrawSphere(idek, 0.1f);
		//if (stuff.Length == 0) UnityEditor.Handles.color = Color.white;
		//else UnityEditor.Handles.color = Color.red;
		//UnityEditor.Handles.Label(new Vector2(idek.x - 0.5f, idek.y - 0.5f), "mpos = " + idek.x + " | " + idek.y);
	}
}
