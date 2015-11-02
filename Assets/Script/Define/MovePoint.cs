using UnityEngine;
using System.Collections;

public class MovePointSerializable
{
	public float x;
	public float y;
	public float Angle;
	public MovePointSerializable()
	{

	}
	public MovePointSerializable(MovePoint point)
	{
		x = point.Position.x;
		y = point.Position.y;
		Angle = point.Angle;
	}
}

[System.Serializable]
public class MovePoint 
{
	public Vector2 Position;
	public float Angle;

	public MovePoint()
	{

	}
	public MovePoint(MovePoint point)
	{
		Position = point.Position;
		Angle  = point.Angle;
	}

	public MovePoint(float x, float y, float angle)
	{
		Position = new Vector2(x,y);
		Angle = angle;
	}

	public MovePoint(Vector2 position, float angle)
	{
		Position = position;
		Angle = angle;
	}

//	public MovePoint(Vector2 position)
//	{
//		Position = position;
//		Angle = 0f;
//	}
//
//	public MovePoint(float x, float y)
//	{
//		Position = new Vector2(x,y);
//		Angle = 0f;
//	}
}
