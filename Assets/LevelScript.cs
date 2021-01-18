using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}
