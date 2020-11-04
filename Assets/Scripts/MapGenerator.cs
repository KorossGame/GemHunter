using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform groundPrefab;
    public Vector2 map;
    
    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        for (int i=0; i<map.x; i++)
        {
            for (int j=0; j<map.y; j++)
            {
                Vector3 tilePos = new Vector3(-map.x / 2 + i, 0, -map.y / 2 + j);
                Transform newTile = Instantiate(groundPrefab, tilePos, Quaternion.identity, this.transform);
            }
        }
    }
}
