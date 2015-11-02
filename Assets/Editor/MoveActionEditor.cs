using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MoveAction))]
public class MoveActionEditor : Editor
{
	private MoveAction _target;
	public void OnEnable()
	{
		_target = target as MoveAction;
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		//_target.transform.localPosition =  _target.ActionData.Offset;
	}

//	public void OnSceneGUI()
//	{
//		_target.ActionData.Offset = _target.transform.localPosition;
//	}
}
