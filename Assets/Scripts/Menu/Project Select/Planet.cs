using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	public Material select;
	public Material defMat;
	bool selected = false;

	public void Selected()
	{
		gameObject.GetComponent<MeshRenderer>().material = select;
		selected = true;
	}

	public void DeSelected(bool ignore, bool isplan)
	{
		if (!selected || (selected && !isplan)) return;

		gameObject.GetComponent<MeshRenderer>().material = defMat;
	}
}
