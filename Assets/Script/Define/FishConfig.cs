using UnityEngine;
using System.Collections;

[System.Serializable]
public class FishConfig
{
	public string Name;
	public int  Price;
	public float  HitProbility;
	public float  Speed = 5;
	public Point Center;
	public Point ColliderSize;
	public Point FishSize;
}
