using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRestarter : MonoBehaviour
{
	public GameObject a;
	public GameObject b;
	public void Yes()
	{
		a.SetActive(true);
		b.SetActive(false);
	}
}
