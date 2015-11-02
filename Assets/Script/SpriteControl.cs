using UnityEngine;
using System.Collections;

public class SpriteControl : MonoBehaviour {
	public Material mat;
	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().sharedMaterial = mat;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
