using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}
