using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class LegacyTrace : MonoBehaviour
{
	public List<MovePoint> Points;
	public void OnDrawGizmos()
	{
		if(Points != null)
		{
			List<MovePoint> points = GetPoints();
			Gizmos.color = Color.white;
			for (int i = 0; i < points.Count - 1; i++)
			{
				Vector3 startPoint = new Vector3(points[i].Position.x, points[i].Position.y, 0);
				Vector3 endPoint = new Vector3(points[i+1].Position.x, points[i+1].Position.y, 0);
				Gizmos.DrawLine(startPoint, endPoint);
				Gizmos.DrawSphere(startPoint, HandleUtility.GetHandleSize(transform.position) * 0.04f);
			}
		}
	}

//	public void OnDrawGizmos()
//	{
//		Gizmos.color = Color;
//		List<Vector3> points = GetPoints();
//		for (int i = 0; i < points.Count - 1; i++)
//		{
//			Gizmos.DrawLine(points[i]., points[i + 1]);
//			Gizmos.DrawSphere(points[i], HandleUtility.GetHandleSize(transform.position) * 0.04f);
//		}
//		
////		
////		#if UNITY_EDITOR
////		if (Selection.activeGameObject != gameObject)
////		{
////			foreach (Transform waypoint in Waypoints)
////			{
////				Gizmos.DrawSphere(waypoint.transform.position, HandleUtility.GetHandleSize(transform.position) * 0.05f);
////			}
////		}
////		#endif
//	}

	public List<MovePoint> GetPoints()
	{
		List<MovePoint> ret = new List<MovePoint>();
		foreach (var item in Points) 
		{
			MovePoint pt = new MovePoint(item);
			pt.Position += new Vector2(transform.position.x, transform.position.y);
			ret.Add(pt);
		}
		return ret;
	}
}
