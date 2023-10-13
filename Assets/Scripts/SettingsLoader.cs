using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingsLoader : MonoBehaviour
{
    public static SettingsLoader Instance;
    public SettingsRoot Settings;

    void Awake()
    {
        Instance = this;
        string path = Application.dataPath + "/" + "GameSettings.json";

#if !UNITY_EDITOR
            path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)  + "/Configs/Configs"  + "/" + "GameSettings.json";
#endif

        if (File.Exists(path))
        {
            Debug.Log("Loading File");
            string jsonString = File.ReadAllText(path);
            Settings = JsonUtility.FromJson<SettingsRoot>(jsonString);
        }
        else
        {
            Debug.LogError("No File");
        }
    }

    [System.Serializable]
    public class SettingsRoot
    {
        public string GameTime;
        public string MovmentSpeed;
        public string ShootTimer;
        public string CollectionTimer;
        public string HpSmallEnemy;
        public string HpBigEnemy;
        public string SpawnChanceOfGoodToBad;
        public string SpawnChanceOfBigBad;
    }

}
