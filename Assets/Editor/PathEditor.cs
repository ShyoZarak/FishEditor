using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using LitJson;


[CustomEditor(typeof(Track))]
public class PathEditor : UnityEditor.Editor
{
	private Track _path;
	private Transform _moreTarget;
	
	private const string NamePrefix = "Waypoint";
	private const int DefaultLineCounts = 25;
	public void OnEnable()
	{
		_path = target as Track;
	}
	
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical();
		DrawWaypointsGUI();
		if (_path.Waypoints.Any())
			DrawSettings();
		EditorGUILayout.EndVertical();
		EditorUtility.SetDirty(_path);
	}
	
	private void DrawWaypointsGUI()
	{
		if (_path.Waypoints.Any())
		{
			GUILayout.BeginVertical("Box");
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Waypoints:", GUILayout.Width(120));
			if (GUILayout.Button("+", GUILayout.Width(40)))
			{
				AddWaypoint(0);
				NormalizeWaypoints();
			}
			
			if(GUILayout.Button("Export", GUILayout.Width(80)))
			{
				ExportWayPoints();
			}
			
			GUILayout.FlexibleSpace();      
			
			EditorGUILayout.EndHorizontal();
			
			for (int i = 0; i < _path.Waypoints.Count; i++)
			{
				Transform waypoint = _path.Waypoints[i];
				
				EditorGUILayout.BeginHorizontal();
				
				_path.Waypoints[i] = EditorGUILayout.ObjectField(waypoint, typeof(Transform), true, GUILayout.Width(120)) as Transform;
				
				if (GUILayout.Button("+", GUILayout.Width(40)))
				{
					AddWaypoint(i + 1);
					NormalizeWaypoints();
				}
				
				if (GUILayout.Button("-", GUILayout.Width(40)))
				{
					RemoveWaypoint(i);
					DestroyImmediate(waypoint.gameObject);
					NormalizeWaypoints();
					break;
				}
				
				if(i < _path.Waypoints.Count -1 && i< _path.UnitPerStep.Count -1)
				{
					_path.UnitPerStep[i] = Mathf.Max(1, EditorGUILayout.IntField("UnitPerStep:", _path.UnitPerStep[i]));
				}
				
				//                    if (GUILayout.Button("More", GUILayout.Width(40)))
				//                    {
				//                        _moreTarget = _moreTarget == waypoint ? null : waypoint;
				//                    }
				
				EditorGUILayout.EndHorizontal();
				
				//                    DrawMoreActions(waypoint);
				//					DrawDelayWaypoint(waypoint);
				// DrawRandomWaypoint(waypoint);
			}
			GUILayout.EndVertical();
		}
		else
		{
			DrawAddStartWaypoints();
		}
	}
	
	private void ExportWayPoints()
	{
		//TraceConfig config = new TraceConfig();
		List<MovePoint> config = _path.GetMovePoints();
		List<MovePointSerializable> configSerialize = new List<MovePointSerializable>();
		foreach (var item in config) 
		{
			MovePointSerializable tmp = new MovePointSerializable(item);
			configSerialize.Add(tmp);
		}
		TraceConfigSerializable result = new TraceConfigSerializable{MovePoints = configSerialize};
		DataAccessor.SaveObjectToJsonFile<TraceConfigSerializable>(result, DataAccessor.GetTracePathByID(System.Convert.ToInt32(_path.name)));
//		List<MovePoint> wayPoints = new List<MovePoint>();
//		List<Vector3> points = _path.GetPoints();
//		for (int i = 0; i < points.Count; i++) 
//		{
//			if(i < points.Count-1)
//			{
//				wayPoints.Add(new MovePoint(points[i], GetAngle(points[i], points[i+1])));
//			}
//			else
//			{
//				wayPoints.Add(new MovePoint(points[i]));
//			}
//		}
//		string json = JsonMapper.ToJson(wayPoints);
//		string savePath = EditorUtility.SaveFilePanel("Save Way Points", string.Empty, "data", "dat");
//		System.IO.TextWriter tw = new System.IO.StreamWriter(savePath);
//		if(tw == null)
//		{
//			Debug.LogError("Cannot write to " + savePath);
//			return;
//		}
//		tw.Write(json);
//		tw.Flush();
//		tw.Close();
	}

	public static float GetAngle(Vector2 p1, Vector2 p2)
	{
		Vector2 direction = (p2 - p1).normalized;
		float angle = direction.y > 0
			? Vector2.Angle(new Vector2(1, 0), direction)
				: -Vector2.Angle(new Vector2(1, 0), direction);
		return angle;
	}
	
	//        private void DrawMoreActions(Transform waypoint)
	//        {
	//            if (waypoint == _moreTarget)
	//            {
	//                GUILayout.BeginHorizontal();
	//                if (GUILayout.Button("Add delay"))
	//                {
	//                    DelayWaypoint delayWaypoint = waypoint.GetComponent<DelayWaypoint>();
	//                    if (delayWaypoint == null)
	//                    {
	//                        delayWaypoint = waypoint.gameObject.AddComponent<DelayWaypoint>();
	//                        delayWaypoint.Delay = 1f;
	//                    }
	//                }
	//	            if (GUILayout.Button("Add random"))
	//	            {
	//		            RandomWaypoint randomWaypoint = waypoint.GetComponent<RandomWaypoint>();
	//		            if (randomWaypoint == null)
	//		            {
	//			            randomWaypoint = waypoint.gameObject.AddComponent<RandomWaypoint>();
	//			            randomWaypoint.RadiusX = Camera.main.orthographicSize/5f;
	//			            randomWaypoint.RadiusY = Camera.main.orthographicSize/5f;
	//		            }
	//	            }
	//                GUILayout.EndHorizontal();
	//            }
	//        }
	
	//		private void DrawDelayWaypoint(Transform waypoint)
	//		{
	//			DelayWaypoint delayWaypoint = waypoint.GetComponent<DelayWaypoint>();
	//			if (delayWaypoint != null)
	//			{
	//				GUILayout.BeginVertical("Box");
	//				GUILayout.Label("Delay waypoint", EditorStyles.boldLabel);
	//				delayWaypoint.Delay = EditorGUILayout.FloatField("Delay", delayWaypoint.Delay);
	//				EditorUtility.SetDirty(delayWaypoint);
	//
	//				if (GUILayout.Button("Delete"))
	//				{
	//					DestroyImmediate(delayWaypoint);
	//					EditorUtility.SetDirty(waypoint);
	//				}
	//
	//				GUILayout.EndVertical();
	//			}
	//		}
	
	//	    private void DrawRandomWaypoint(Transform waypoint)
	//	    {
	//		    RandomWaypoint randomWaypoint = waypoint.GetComponent<RandomWaypoint>();
	//		    if (randomWaypoint != null)
	//		    {
	//				GUILayout.BeginVertical("Box");
	//				GUILayout.Label("Random waypoint", EditorStyles.boldLabel);
	//			    randomWaypoint.RadiusX = EditorGUILayout.FloatField("RadiusX", randomWaypoint.RadiusX);
	//				randomWaypoint.RadiusY = EditorGUILayout.FloatField("RadiusY", randomWaypoint.RadiusY);
	//				randomWaypoint.Rotation = EditorGUILayout.FloatField("Rotation", randomWaypoint.Rotation);
	//				EditorUtility.SetDirty(randomWaypoint);
	//
	//				if (GUILayout.Button("Delete"))
	//				{
	//					DestroyImmediate(randomWaypoint);
	//					EditorUtility.SetDirty(waypoint);
	//				}
	//
	//				GUILayout.EndVertical();
	//		    }
	//	    }
	
	private void DrawAddStartWaypoints()
	{
		GUILayout.Space(10);
		if (GUILayout.Button("Add Start Waypoints"))
		{
			AddWaypoint(0);
			AddWaypoint(1);
			NormalizeWaypoints();
		}            
	}
	
	private void NormalizeWaypoints()
	{
		UpdateWaypointNames();
		ResetWaypointsZCoordinate();
	}
	
	private void UpdateWaypointNames()
	{
		for(int i = 0; i < _path.Waypoints.Count; i++)
		{
			if(IsMatchPattern(_path.Waypoints[i].name))
			{
				_path.Waypoints[i].name = string.Format("{0}{1:000}", NamePrefix, i + 1);
			}
		}
	}
	
	private void ResetWaypointsZCoordinate()
	{
		foreach (Transform waypoint in _path.Waypoints)
			waypoint.position = new Vector3(waypoint.position.x, waypoint.position.y, 0f);
	}
	
	private bool IsMatchPattern(string name)
	{
		return name.StartsWith(NamePrefix);
	}
	
	private void AddWaypoint(int ind)
	{
		_path.Waypoints.Insert(ind, CreateWaypoint(ind));
		_path.BezierPoints.Insert(ind, new Vector3());
		_path.UnitPerStep.Insert(ind, DefaultLineCounts);
	}
	
	private Transform CreateWaypoint(int ind)
	{
		GameObject newWaypoint = new GameObject{name = NamePrefix};
		newWaypoint.transform.SetParent(_path.transform);
		SetPositionForNewWaypoint(newWaypoint.transform, ind);
		return newWaypoint.transform;
	}
	
	private void RemoveWaypoint(int ind)
	{
		if(ind < _path.Waypoints.Count)
		{
			_path.Waypoints.RemoveAt(ind);
		}
		
		if(ind < _path.BezierPoints.Count)
		{
			_path.BezierPoints.RemoveAt(ind);
		}
		
		if(ind < _path.UnitPerStep.Count)
		{
			_path.UnitPerStep.RemoveAt(ind);
		}
	}

	private float CameraSize
	{
		get
		{
			if(Camera.main != null)
			{
				return Camera.main.orthographicSize;
			}
			else
			{
				return 100f;
			}
		}
	}
	private void SetPositionForNewWaypoint(Transform newWaypoint, int ind)
	{
		if (_path.Waypoints.Count == 0)
		{
			newWaypoint.localPosition = new Vector3(-1, -1, 0)*CameraSize;
		}
		else if (_path.Waypoints.Count == 1)
		{
			newWaypoint.localPosition = new Vector3(1, 1, 0)*CameraSize;
		}
		else if(ind == _path.Waypoints.Count)
		{
			newWaypoint.position = 1.5f*_path.Waypoints[ind - 1].position - _path.Waypoints[ind - 2].position/2f;
		}
		else if (ind == 0)
		{
			newWaypoint.position = 1.5f*_path.Waypoints[ind].position - _path.Waypoints[ind + 1].position/2f;
		}
		else
		{
			newWaypoint.position = (_path.Waypoints[ind].position + _path.Waypoints[ind - 1].position)/2f;
		}
	}
	
	private void DrawSettings()
	{
		_path.IsShowLinePoints = EditorGUILayout.Toggle("IsShowLinePoints", _path.IsShowLinePoints);
		//_path.IsCurved = EditorGUILayout.Toggle("Curved", _path.IsCurved);
		//	        if (_path.IsCurved)
		//		        _path.LinesCount = Mathf.Clamp(EditorGUILayout.IntField("Lines count", _path.LinesCount), 1, 1000);
		_path.Color = EditorGUILayout.ColorField("Color", _path.Color);
	}
	
	public void OnSceneGUI()
	{
		for(int i =0; i < _path.Waypoints.Count; i++)
		{
			Handles.color = Color.white;
			_path.Waypoints[i].position = Handles.FreeMoveHandle(
				_path.Waypoints[i].position, Quaternion.identity,
				HandleUtility.GetHandleSize(_path.Waypoints[i].position) * 0.1f, Vector3.one, Handles.CubeCap);
			
			//if (_path.IsCurved)
			{
				Handles.color = Color.black;
				_path.SetHandle1(i, Handles.FreeMoveHandle(_path.GetHandle1(i), Quaternion.identity,
				                                           HandleUtility.GetHandleSize(_path.GetHandle1(i)) * 0.1f, Vector3.one, Handles.CircleCap));
				
				Handles.DrawLine(_path.GetHandle1(i), 2 * _path.Waypoints[i].position - _path.GetHandle1(i));
			}
			
			//				RandomWaypoint randomWaypoint = _path.Waypoints[i].GetComponent<RandomWaypoint>();
			//				if (randomWaypoint != null)
			//					RandomWaypointEditor.DrawSceneGUI(randomWaypoint);
		}
	}
}
