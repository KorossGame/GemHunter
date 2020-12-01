using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public bool available;
	public Vector3 worldPosition;

	public Node(bool availability, Vector3 worldPos)
	{
		available = availability;
		worldPosition = worldPos;
	}
}
