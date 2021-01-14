using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class BossLevelEnter : MonoBehaviour
{
    [SerializeField] private GameObject cinematicCamera;

    public PlayableDirector playableDirector;
    public Boss boss;

    private void OnEnable()
    {
        playableDirector.stopped += afterCinematic;
    }

    private void OnDisable()
    {
        playableDirector.stopped -= afterCinematic;
    }

    private void Start()
    {
        SetupPlayer();
        StartCoroutine(PlayCinematic());
    }

    private IEnumerator PlayCinematic()
    {
        yield return new WaitForSeconds(1f);
        playableDirector.Play();
    }

    private void afterCinematic(PlayableDirector aDirector)
    {
        cinematicCamera.SetActive(false);
        PlayerManager.instance.player.SetActive(true);
        boss.activated = true;
    }

    private void SetupPlayer()
    {
        PlayerManager.instance.player.transform.position = new Vector3(0, 0.5f, 0);
        PlayerManager.instance.player.SetActive(false);
    }

}
