using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private byte phase;

    void Start()
    {
        HP = 500;
        Speed = 5f;
        PathQueueManager.RequestPath(transform.position, target.transform.position, OnPathFound);
    }
}
