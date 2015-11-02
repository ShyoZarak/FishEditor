using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

[CustomEditor(typeof(LegacyTrace))]
public class LegacyTraceEditor : Editor
{
	public static string FileDefaultDirectory
	{
		get
		{
			return PlayerPrefs.GetString("FileDefaultDirectory", string.Empty);
		}
		set
		{
			PlayerPrefs.SetString("FileDefaultDirectory", value);
		}
	}
	LegacyTrace targetTrace;
	public override void OnInspectorGUI ()
	{
		targetTrace = target as LegacyTrace;
		if(GUILayout.Button("Import"))
		{
			string filePath = EditorUtility.OpenFilePanel("导入老数据", FileDefaultDirectory, "dat");
			targetTrace.Points = ReadTrackFile(filePath);
			targetTrace.name = Path.GetFileNameWithoutExtension(filePath);
		}

		if(GUILayout.Button("Export"))
		{
			int id = Convert.ToInt32(targetTrace.name);
			string path = DataAccessor.GetTracePathByID(id);
			List<MovePointSerializable> points = new List<MovePointSerializable>();

			foreach (var item in targetTrace.GetPoints()) 
			{
				points.Add(new MovePointSerializable(item));
			}
			TraceConfigSerializable config = new TraceConfigSerializable{MovePoints = points};
			DataAccessor.SaveObjectToJsonFile<TraceConfigSerializable>(config, path);
		}

		base.OnInspectorGUI ();
	}

//	public void OnSceneGUI()
//	{
//		if(targetTrace != null)
//		{
//			Vector2 offset = targetTrace.transform.position;
//			if(targetTrace.Points != null)
//			{
//				foreach (var item in targetTrace.Points) 
//				{
//					item.Position +=offset;
//				}
//			}
//		}
//
//	}
	
	private List<MovePoint> ReadTrackFile(string filePath)
	{
		List<Vector3> Points = new List<Vector3>();
		if(string.IsNullOrEmpty(filePath))
		{
			return null;
		}
		
		if(!File.Exists(filePath))
		{
			Debug.LogError(string.Format("filePath {0} is not exist", filePath));
			return null;
		}
		
		FileDefaultDirectory = Path.GetDirectoryName(filePath);
		
		StreamReader sr = File.OpenText(filePath);
		string str = "";
		string text = "";
		int num = 0;
		while ((str = sr.ReadLine()) != null)
		{
			if(str[0] != '(')
			{
				continue;
			}
			
			str = System.Text.RegularExpressions.Regex.Replace(str, @"(.*\()(.*)(\).*)", "$2");
			string[] ss = str.Split(',');
			Vector3 point = new Vector3(System.Convert.ToSingle(ss[0]), System.Convert.ToSingle(ss[1]), System.Convert.ToSingle(ss[2])+270f);
			Points.Add(point);
		}
		sr.Close();
		List<MovePoint> movePoints = new List<MovePoint>();
		for(int i=0; i<Points.Count; i++)
		{
			movePoints.Add(new MovePoint{Position = Points[i] , Angle = Points[i].z});
		}
	 
		return movePoints;
	}
}
