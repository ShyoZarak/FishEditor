//using UnityEngine;
//using System.Collections;
//using UnityEditor;
//using System.Collections.Generic;
//public class FishEditorWin : EditorWindow 
//{
//	[MenuItem("Window/Fish")]
//	static void Init()
//	{
//		EditorWindow.GetWindow<FishEditorWin>("FishEditorWin");
//	}
//
//	Vector2 m_scrollPose = Vector2.zero;
//	List<Fish> Fishes = new List<Fish>();
//
//	private static GUIStyle m_line = null;
//	
////	//constructor
////	static LOGDGUIStyles()
////	{
////		
////		m_line = new GUIStyle("box");
////		m_line.border.top = m_line.border.bottom = 1;
////		m_line.margin.top = m_line.margin.bottom = 1;
////		m_line.padding.top = m_line.padding.bottom = 1;
////	}
//	
//	public static GUIStyle EditorLine
//	{
//		get 
//		{
//			if(m_line == null)
//			{
//				m_line = new GUIStyle("box");
//				m_line.border.top = m_line.border.bottom = 1;
//				m_line.margin.top = m_line.margin.bottom = 1;
//				m_line.padding.top = m_line.padding.bottom = 1;
//			}
//			return m_line; 
//		}
//	}
//	void OnGUI()
//	{
//
//		if(GUILayout.Button("Add"))
//		{
//			Fish newFish = new Fish();
//			Fishes.Add(newFish);
//		}
//
//		m_scrollPose = EditorGUILayout.BeginScrollView(m_scrollPose);
//		EditorGUILayout.BeginVertical();
//		for(int i=0; i<Fishes.Count; i++)
//		{
//			Fish fish = Fishes[i];
//			EditorGUILayout.Separator();
//			fish.Name = EditorGUILayout.TextField("FishName", fish.Name);
//			fish.FishType = EditorGUILayout.IntField("FishType", fish.FishType);
//			fish.FishSprite = EditorGUILayout.ObjectField("增加一个贴图", fish.FishSprite, typeof(Texture2D), true) as Texture2D;
//
//
//			fish.Width = EditorGUILayout.FloatField("Width", fish.Width);
//			fish.Height = EditorGUILayout.FloatField("Height", fish.Height);
//			fish.Price = EditorGUILayout.IntField("Price", fish.Price);
//			fish.HitProbility = EditorGUILayout.IntField("HitProbility", fish.HitProbility);
//			if(GUILayout.Button("Editor"))
//			{
//				GameObject obj = new GameObject();
//				obj.AddComponent<SpriteRenderer>();
//				SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
// 				
//				if(fish.FishSprite != null)
//				{
//					string path= AssetDatabase.GetAssetPath(fish.FishSprite);
//					Sprite sp = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
//					spr.sprite = sp; //注意居中显示采用0.5f值
//				}
//				//Sprite sp = Sprite.Create(fish.FishSprite, new Rect(0, 0, fish.FishSprite.width, fish.FishSprite.height) ,new Vector2(0.5f,0.5f));
//			
//			}
//
//			GUILayout.Box(GUIContent.none, EditorLine , GUILayout.ExpandWidth(true), GUILayout.Height(1f));
//			EditorGUILayout.Space();
//		}
//
//		EditorGUILayout.EndVertical();
//		EditorGUILayout.EndScrollView();
//	}
//}
