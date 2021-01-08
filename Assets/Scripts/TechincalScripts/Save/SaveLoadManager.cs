using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveLoadManager
{
    public string name;

    public SaveLoadManager() { }

    public void CollectDataToSave()
    {
        name = "Hello";
    }

    public void LoadDataToGame()
    {
        name = name+"test";
    }
}