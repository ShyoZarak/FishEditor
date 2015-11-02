using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveAction : MonoBehaviour {

	public MoveActionData ActionData;
	private List<MovePoint> _movePoints = null;
	private List<MovePoint> MovePoints
	{
		get
		{
			if(_movePoints == null)
			{
				TraceConfig config = GameConfig.GetTraceConfig(ActionData.TraceID);
				_movePoints = new List<MovePoint>();
				foreach (var item in config.MovePoints)
				{
					MovePoint p = new MovePoint(item);
					p.Position += ActionData.Offset;
					_movePoints.Add(p);
				}
			}
			return _movePoints;
		}
	}

	private FishConfig configFish
	{
		get
		{
			return GameConfig.GetFishConfig(ActionData.FishType);
		}
	}

	float beginTime = 0;
	// Use this for initialization
	void Start () 
	{
		beginTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		Update(Mathf.Min(1f, Mathf.Max(0, (Time.time-beginTime - ActionData.Delay)*configFish.Speed / MovePoints.Count)));
	}

	void Update(float time)
	{
		float fDiff;
		float fIndex = time * MovePoints.Count;
		int index =  (int)fIndex;
		
		fDiff = fIndex - index;
		
		if (index >= MovePoints.Count)
		{
			index = MovePoints.Count - 1;
		}
		
		MovePoint move_point = new MovePoint();
		
		if (index<MovePoints.Count-1)
		{
			MovePoint move_point1 = MovePoints[index];
			MovePoint move_point2 = MovePoints[index+1];
			
			move_point.Position = Vector2.Lerp(move_point1.Position, move_point2.Position, fDiff);
			move_point.Angle = Mathf.LerpAngle(move_point1.Angle, move_point2.Angle, fDiff);
		}
		else
		{
			move_point = MovePoints[index];
		}

		transform.position = move_point.Position;
		transform.rotation = Quaternion.identity;
		transform.Rotate(transform.forward, move_point.Angle);
	}
}
