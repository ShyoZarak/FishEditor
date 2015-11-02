using UnityEngine;
using System.Collections;

public class MoveActionSerializeData
{
	public int 	 FishType;
	public int 	 TraceID;
	public float Delay = 0;
	public Point Offset;
	public MoveActionSerializeData(MoveActionData data)
	{
		FishType = data.FishType;
		TraceID = data.TraceID;
		Delay = data.Delay;
		Offset = new Point{x = data.Offset.x, y = data.Offset.y};
	}
}

[System.Serializable]
public class MoveActionData
{
	public int FishType;
	public int TraceID;
	public float Delay = 0;
	public Vector2 Offset = Vector2.zero;
}
