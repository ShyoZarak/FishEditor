using UnityEngine;
using System.Collections;

public interface IMoveAction 
{
	bool Pause
	{
		get;
	 	set;
	}

	bool IsDone 
	{
		get;
	}

	float Speed
	{
		get;
		set;
	}
}
