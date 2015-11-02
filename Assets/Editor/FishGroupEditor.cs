using UnityEngine;
using System.Collections;
using UnityEditor;
using JsonDotNet;
using Newtonsoft.Json;
using System.Collections.Generic;

public static class MyGUIStyles
{
	private static GUIStyle m_line = null;

	public static GUIStyle EditorLine
	{
		get 
		{ 
			if(m_line == null)
			{
				m_line = new GUIStyle("box");
				m_line.border.top = m_line.border.bottom = 1;
				m_line.margin.top = m_line.margin.bottom = 1;
				m_line.padding.top = m_line.padding.bottom = 1;
			}
			return m_line;
		}
	}
}

[CustomEditor(typeof(FishGroup))]
public class FishGroupEditor : Editor
{
	private FishGroup _group;
	public void OnEnable()
	{
		_group = target as FishGroup;
	}

	public override void OnInspectorGUI ()
	{
		if(GUILayout.Button("Export"))
		{
			Export();
		}
	}

	private void Export()
	{
		List<MoveActionData> config = new List<MoveActionData>();
		if(_group != null)
		{
			int id = System.Convert.ToInt32(_group.name);
			for(int i=0; i< _group.transform.childCount; i++)
			{
				Transform tran = _group.transform.GetChild(i);
				MoveAction action = tran.GetComponent<MoveAction>();
				if(action != null)
				{
					config.Add(action.ActionData);
				}

			}
			List<MoveActionSerializeData> serializeData = new List<MoveActionSerializeData>();
			foreach (var item in config)
			{
				serializeData.Add(new MoveActionSerializeData(item));
			}
			DataAccessor.SaveObjectToJsonFile<List<MoveActionSerializeData>>(serializeData, DataAccessor.GetGroupFishPathByID(id));
		}
	}
//	public override void OnInspectorGUI ()
//	{
//		EditorGUILayout.BeginVertical();
//		EditorGUILayout.BeginHorizontal();
//		if(GUILayout.Button("Add", GUILayout.Width(40)))
//		{
//			AddFish();
//		}
//
//		if(GUILayout.Button("Export", GUILayout.Width(80)))
//		{
//			ExportGruopFish();
//		}
//		EditorGUILayout.EndHorizontal();
//		   
//		for(int i=0; i<_group.Fishes.Count; i++)
//		{
//		
//			EditorGUILayout.ObjectField("Binding Transform", _group.Fishes[i], typeof(Transform), true);
//			if(GUILayout.Button("-", GUILayout.Width(40)))
//			{
//				RemoveFish(i);
//				return;
//			}
//
//			_group.Configs[i].FishType = EditorGUILayout.IntField("FishType", _group.Configs[i].FishType);
//			_group.Configs[i].Delay = Mathf.Max(0, EditorGUILayout.FloatField("Delay", _group.Configs[i].Delay));
//			_group.Configs[i].Offset = _group.Fishes[i].transform.localPosition;
//			_group.Fishes[i].transform.localPosition = EditorGUILayout.Vector2Field("Offset",_group.Configs[i].Offset);
//			_group.Configs[i].TrackID = EditorGUILayout.IntField("TrackID", _group.Configs[i].TrackID);
//			GUILayout.Box(GUIContent.none, MyGUIStyles.EditorLine , GUILayout.ExpandWidth(true), GUILayout.Height(1f));
//		}
//
//		EditorGUILayout.EndVertical();
//		EditorUtility.SetDirty(_group);
//	}
//
//	private void AddFish()
//	{
//		GameObject newfish = new GameObject("Fish");
//		newfish.transform.SetParent(_group.transform, false);
//		_group.Fishes.Add(newfish.transform);
//		_group.Configs.Add(new MoveConfig());
//	}
//
//	private void RemoveFish(int index)
//	{
//		Transform trans = _group.Fishes[index];
//		_group.Fishes.RemoveAt(index);
//		_group.Configs.RemoveAt(index);
//		DestroyImmediate(trans.gameObject);
//	}
//
//	private void ExportGruopFish()
//	{
//		string path = DataAccessor.GroupFishDirectory + _group.name + ".dat";
//		DataAccessor.SaveObjectToJsonFile<List<MoveConfig>>(_group.Configs, path);
//	}
}
