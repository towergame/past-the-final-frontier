using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ComponentBase
{
	public string modelName;
	public int tier;

	public List<string> requiredFlags;

	public int cost;
	public int additionalWeight;
	public int additionalForce;
	public int additionalFuel;
	public float timeSslash;
	public float forceSlash;
}
