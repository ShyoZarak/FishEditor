using UnityEngine;
using System.Collections;

public class SortOrder : MonoBehaviour {
	public int Order;
	void Awake()
	{
		GetComponent<Renderer>().sortingOrder = Order;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
