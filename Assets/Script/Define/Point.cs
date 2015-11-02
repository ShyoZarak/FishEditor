using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Point 
{
	public float x;
	public float y;

	public Point(Vector2 point)
	{
		x = point.x;
		y = point.y;
	}

	public Point(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public Vector2 ToVector2()
	{
		return new Vector2(x,y);
	}
}
