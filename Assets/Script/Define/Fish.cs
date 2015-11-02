using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Fish))]
public class FishEditor:Editor
{
	public override void OnInspectorGUI ()
	{
		GUILayout.Button("hello");
		base.OnInspectorGUI ();
	}
}

public class Fish : ScriptableObject
{
	public string FishSprite;
	public string Name;
	public int    FishType;
	public float  Width;
	public float  Height;
	public int    Price;
	public int    HitProbility;
	public float  Speed;
	public Sprite SS;
	public Texture2D TT;
	public AnimationCurve curve;

	[MenuItem("example/load/ScriptableObjectTest/pack Asset Data")]
	private static void exportDesignerAssetData()
	{
		string path = "Assets/ScriptableObjectTest";
		string name = "DesignerData";
		
		DirectoryInfo dirInfo = new DirectoryInfo(path);
		if(!dirInfo.Exists)
		{
			Debug.LogError(string.Format("can found path={0}", path));
			return;
		}
		
		// ScriptableObject对象要用ScriptableObject.CreateInstance创建
		Fish ddata = ScriptableObject.CreateInstance<Fish>();
		//ddata.init();
		
		// 创建一个asset文件
		string assetPath = string.Format("{0}/{1}.asset", path, name);
		AssetDatabase.CreateAsset(ddata, assetPath);
		
		// 创建一个assetbundle文件
		//string assetbundlePath = string.Format("{0}/{1}.assetbundle", path, name);
		//BuildPipeline.BuildAssetBundle(ddata, null, assetbundlePath);
		
		Debug.Log("Finish!");

		Fish itemmanager = (Fish)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Fish));
	}
}


