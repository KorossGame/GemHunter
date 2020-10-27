using System.Collections;
using UnityEngine;

abstract public class Enemy : Subject
{
    public Transform target;
    protected Vector3[] path;
    protected int targetIndex;

    private int powerUPcount = 3;
    private float powerUPdropChange = 0.1f;
    private float maxValue = 1;

    protected void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    protected IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Speed * Time.deltaTime);
            yield return null;
        }
    }

    protected void GeneratePowerUP()
    {
        // Set random seed
        Random.InitState((int)System.DateTime.Now.Ticks);
        
        // Generate new number to define if generate powerUP is needed
        int randomNumber = (int)Random.Range(0, maxValue);

        // Calc if we got value in drop chance
        if (randomNumber > maxValue * powerUPdropChange) return;

        // Which powerUP going to be dropped
        int randomPowerUP = Random.Range(0, powerUPcount);
        // Instanciate(powerUP, transform.position, transform.rotation);
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i<path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                    Gizmos.DrawLine(transform.position, path[i]);
                else
                    Gizmos.DrawLine(path[i - 1], path[i]);
            }
        }
    }
}
