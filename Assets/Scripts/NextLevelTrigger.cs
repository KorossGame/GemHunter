using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelTrigger : MonoBehaviour
{
    public Text tpNextLevelText;

    private bool activated = false;

    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("LoadingScreen");
                activated = false;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        tpNextLevelText.gameObject.SetActive(true);
        activated = true;
    }

    private void OnTriggerExit(Collider col)
    {
        tpNextLevelText.gameObject.SetActive(false);
        activated = false;
    }
}
