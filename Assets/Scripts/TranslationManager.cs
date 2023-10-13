using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using static TextScript;

public class TranslationManager : MonoBehaviour
{
    public static TranslationManager Instance;
    private Root jsonRoot;
    public bool forceOneLanugage;
    public Langugage forcedLanguage;

    [HideInInspector] public List<TextScript> textsAll = new List<TextScript>();

    void Awake()
    {
        //  TextAsset jsonFile = Resources.Load<TextAsset>(Application.streamingAssetsPath + "/" + "Texts");
        Instance = this;

        string path = Application.dataPath + "/" + "Texts.json";

#if !UNITY_EDITOR
        path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "/Configs/Texts" + "/" + "Texts.json";
#endif

        if (File.Exists(path))
        {
            Debug.Log("Loading File");
            string jsonString = File.ReadAllText(path);
            jsonRoot = JsonUtility.FromJson<Root>(jsonString);
        }
        else
        {
            Debug.LogError("No File");
        }
    }

    public void ChangeForcedLangugeAsInt(int indexOfLanguage)
    {
        forcedLanguage = (Langugage)indexOfLanguage;

        foreach (TextScript text in textsAll)
        {
            text.ChangeLanguage(forcedLanguage);
        }
    }

    public void ChangeToNextLang()
    {
        int languagesCount = Enum.GetNames(typeof(Langugage)).Length;
        int forcedLanguageInt = (int)forcedLanguage;

        forcedLanguageInt++;
        if (forcedLanguageInt >= languagesCount)
        {
            ChangeForcedLangugeAsInt(0);
        }
        else
        {
            ChangeForcedLangugeAsInt(forcedLanguageInt);
        }

    }

    public string GetText(string category, string variable, string language)
    {
        foreach (Category c in jsonRoot.categories)
        {
            if (c.name == category)
            {
                foreach (Variable var in c.variables)
                {
                    if (var.name == variable)
                    {
                        foreach (Value val in var.values)
                        {
                            if (val.lang == language)
                            {
                                return val.text;
                            }
                        }
                    }
                }
            }
        }
        return "ERROR WRONG TRANSLATION CODE";
    }
}

[System.Serializable]
public class Category
{
    public string name;
    public Variable[] variables;
}

[System.Serializable]
public class Root
{
    public Category[] categories;
}

[System.Serializable]
public class Value
{
    public string text;
    public string css;
    public string lang;
}

[System.Serializable]
public class Variable
{
    public string name;
    public Value[] values;
}