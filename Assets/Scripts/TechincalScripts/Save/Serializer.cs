using UnityEngine;
using System.Collections;
using System.IO;

public class Serializer : MonoBehaviour
{
    // Name of save file & encryption settings
    private readonly string SAVE_FILE = "player.dat";
    private readonly string IV_KEY = "4437772310107691";
    private readonly string ENCRYPTION_KEY = "aPdSgVkYp3s6v9y$B&E)H@MbQeThWmZq";

    // AES object to encrypt/decrypt data
    private AES crypto;

    // Object to get/set data
    private SaveLoadManager data;
    private SaveLoadManager copiedData;

    private void Awake()
    {
        crypto = new AES(ENCRYPTION_KEY, IV_KEY);
        data = new SaveLoadManager();
    }

    public void Save()
    {
        // Get all needed to save data
        data.CollectDataToSave();
        
        // Convert data to json
        string json = JsonUtility.ToJson(data);

        // Encrypt data
        byte[] soup = crypto.Encrypt(json);

        // Generate filename
        string filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);

        // If previous save exists delete it
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }

        // Write new save
        File.WriteAllBytes(filename, soup);
    }

    public void Load()
    {
        // Generate filename
        string filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);

        //string jsonFromFile = File.ReadAllText(filename);
        byte[] soupBackIn = File.ReadAllBytes(filename);

        // Decrypt data
        string jsonFromFile = crypto.Decrypt(soupBackIn);

        // Create a copy object
        copiedData = JsonUtility.FromJson<SaveLoadManager>(jsonFromFile);

        // Load saved values
        copiedData.LoadDataToGame();
    }
}
