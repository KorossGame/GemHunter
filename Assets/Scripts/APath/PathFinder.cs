using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinder : MonoBehaviour
{
    PathQueueManager requestManager;
    Grid grid;

    void Awake()
    {
        requestManager = GetComponent<PathQueueManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable)
        {
            // Open set - we have a search in it, but closed stores evaluated nodes
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closeSet = new HashSet<Node>();

            // Add start node to open set
            openSet.Add(startNode);

            // While open set is not empty
            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closeSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closeSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < currentNode.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
            yield return null;
            if (pathSuccess)
            {
                waypoints = RetracePath(startNode, targetNode);
            }
            requestManager.FinishProcessingPath(waypoints, pathSuccess);
        }
    }

    Vector3[] RetracePath (Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent; 
        }

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
        
    }

    Vector3[] SimplifyPath (List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i=1; i< path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }

            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        // 14 - cost of diagonal move (sqrt 2)*10, 10 - horizontal/vertical move
        return 14 * Mathf.Min(distanceX, distanceY) + 10 * (Mathf.Max(distanceX,distanceY) - Mathf.Min(distanceX, distanceY));
    }
}
