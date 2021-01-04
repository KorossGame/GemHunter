using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGameManager : MonoBehaviour
{
    public static TutorialGameManager instance;

    void Awake()
    {
        instance = this;
    }

    public static void EnemyPhase()
    {
        // Spawn each type of enemy
    }

    public static void PowerUPPhase()
    {
        // Spawn each type of powerups
    }

    public static void AmmoBoxPhase()
    {
        // Spawn each type of ammo boxes
    }
}
