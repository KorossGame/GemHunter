using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	public static Grid instance;

	// If we want to displya the grid or not
	public bool visible = false;

	// Mask for checking if node is empty
    public LayerMask unwalkableMask;

	// Node size
	public byte cellRadius;
	private int cellDiameter;

	// Customizable world area
	public Vector3 gridWorldSize;
	private int gridXCount, gridZCount;

	// Node List
	public Node[,] nodeList;

	private void Awake()
    {
		instance = this;
		SetupGrid();
		GenerateGrid();
	}

	private void FixedUpdate()
    {
		GenerateGrid();
	}

	private void SetupGrid()
    {
		cellDiameter = cellRadius * 2;

		// Count how many nodes we need to store
		gridXCount = Mathf.RoundToInt(gridWorldSize.x / cellDiameter);
		gridZCount = Mathf.RoundToInt(gridWorldSize.z / cellDiameter);

		// Create new array to store all the nodes
		nodeList = new Node[gridXCount, gridZCount];
	}

	private void GenerateGrid()
	{
		// Calculate the bottolLeft of the world area
		Vector3 bottomLeft = transform.position - (Vector3.right * gridWorldSize.x / 2) - (Vector3.forward * gridWorldSize.z / 2);

		// Save each node
		for (int x = 0; x < gridXCount; x++)
		{
			for (int z = 0; z < gridZCount; z++)
			{
				Vector3 worldPoint = bottomLeft + Vector3.right * (x * cellDiameter + cellRadius) + Vector3.forward * (z * cellDiameter + cellRadius);
				
				// Check if new node is walkable
				bool walkable = !Physics.CheckSphere(worldPoint, cellRadius, unwalkableMask);
				
				// Save the node
				nodeList[x, z] = new Node(walkable, worldPoint);
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (!visible) return;

		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0, gridWorldSize.z));

		if (nodeList != null)
		{
			foreach (Node node in nodeList)
			{
				Gizmos.color = (node.available) ? Color.white : Color.red;
				Gizmos.DrawCube(node.worldPosition, Vector3.one * (cellDiameter * 0.9f));
			}
		}
	}
}
