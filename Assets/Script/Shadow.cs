using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {
	
	public Vector2 Offset = new Vector2(0.4f, 0.4f);
	// Update is called once per frame
	void Update () 
	{
		Vector3 angles = transform.parent.eulerAngles;
		Vector3 offset = Vector3.zero; 
		offset.x -=  Mathf.Sin(angles.z * Mathf.Deg2Rad) * Offset.x;
		offset.y -=  Mathf.Cos(angles.z * Mathf.Deg2Rad) * Offset.y;
		transform.localPosition = offset;
	}
}
