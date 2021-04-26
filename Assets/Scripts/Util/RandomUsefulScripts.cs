using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUsefulScripts
{
	// code champ
	public static Vector3 Vec2ToVec3(Vector2 input, float baseZ = 0)
	{
		return new Vector3(input.x, input.y, baseZ);
	}

	public static Vector2 Vec3ToVec2(Vector3 input)
	{
		return new Vector2(input.x, input.y);
	}

	public static Vector3 UpdateZ(Vector3 input, float newZ)
	{
		Vector3 vec = input;
		vec.z = newZ;
		return vec;
	}
}
