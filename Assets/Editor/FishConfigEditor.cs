using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;

[CustomEditor(typeof(FishConfigWrapper))]
public class FishConfigEditor : Editor
{
	private FishConfigWrapper _target;
	public void OnEnable()
	{
		_target = target as FishConfigWrapper;
	}

	public override void OnInspectorGUI ()
	{
		if(GUILayout.Button("Export"))
		{
			ExportConfig();
		}
		base.OnInspectorGUI ();
	}

	private void ExportConfig()
	{
		_target.Config.Center = new Point
		{
			x =  _target.GetComponent<BoxCollider2D>().center.x,
			y = _target.GetComponent<BoxCollider2D>().center.y
		};

		_target.Config.ColliderSize = new Point
		{ 
			x =  _target.GetComponent<BoxCollider2D>().size.x,
			y = _target.GetComponent<BoxCollider2D>().size.y
		};

		_target.Config.FishSize = new Point
		{
			x = _target.transform.localScale.x,
			y = _target.transform.localScale.y,
		};

		string path = DataAccessor.GetFishPathByType(System.Convert.ToInt32(_target.name));
		DataAccessor.SaveObjectToJsonFile<FishConfig>(_target.Config, path);
	}
}
