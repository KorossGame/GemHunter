using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private AsyncOperation sceneLoad;

    private int loadingScreenID;
    private float timeForReaction = 0.5f;
    private bool animationPlayed = false;

    public Image progressSlider;
    public BlickingText text;

    private void Awake()
    {
        loadingScreenID = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        int nextSceneID = Game.instance.latestGameScene + 1;
        if (nextSceneID != loadingScreenID)
        {
            StartCoroutine(LoadLevelAsync(nextSceneID));
        }

        if (PlayerManager.instance && PlayerManager.instance.player)
        {
            PlayerManager.instance.player.SetActive(false);
        }
    }

    private void Update()
    {
        if (PlayerManager.instance && PlayerManager.instance.player && PlayerManager.instance.gameObject.activeSelf)
        {
            PlayerManager.instance.player.SetActive(false);
        }
    }

    private IEnumerator LoadLevelAsync(int nextSceneID) 
    {
        sceneLoad = SceneManager.LoadSceneAsync(nextSceneID);
        sceneLoad.allowSceneActivation = false;

        // Time for user reaction
        yield return new WaitForSeconds(timeForReaction);

        // Loading progress
        while (!sceneLoad.isDone)
        {
            // Set slider value to progress bar
            progressSlider.fillAmount = sceneLoad.progress;

            // Check if the load has finished more than 90%
            if (sceneLoad.progress >= 0.9f)
            {
                if (!animationPlayed)
                {
                    progressSlider.fillAmount = sceneLoad.progress;
                    yield return new WaitForSeconds(timeForReaction);
                    animationPlayed = true;
                }

                // Disable load
                progressSlider.transform.parent.gameObject.SetActive(false);

                // Activate blicking text
                text.gameObject.SetActive(true);

                //Wait to you press the space key to activate the Scene
                if (Input.anyKeyDown)
                {
                    sceneLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
