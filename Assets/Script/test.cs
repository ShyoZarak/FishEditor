using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public Transform p1;
	public Transform p2;
	// Use this for initialization
	void Start () {

	
	}

	private float GetAngree(Vector2 p1, Vector2 p2)
	{
		Vector2 direction = (p2 - p1).normalized;
		float angle = direction.x > 0
			? -Vector2.Angle(new Vector2(0, 1), direction)
				: Vector2.Angle(new Vector2(0, 1), direction);
		return angle;
	}
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.identity;
		transform.Rotate(transform.forward, GetAngree(p1.position, p2.position));
	}
}
