using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextScript : MonoBehaviour
{
    public enum Langugage
    {
        pl,
        en
    }

    [SerializeField] string category;
    [SerializeField] string variabels;
    [SerializeField] Langugage language;
    TextMeshPro text;
    TextMeshProUGUI textUI;

    private void Start()
    {
        TryGetComponent<TextMeshPro>(out text);
        TryGetComponent<TextMeshProUGUI>(out textUI);

        if (text == null && textUI == null)
        {
            Debug.LogError("TextScript needs TextMeshPro or TextMeshProUGUI");
        }

        TranslationManager.Instance.textsAll.Add(this);

        if (TranslationManager.Instance.forceOneLanugage)
        {
            language = TranslationManager.Instance.forcedLanguage;
        }

        UpdateText();
    }

    public void ChangeLanguage(Langugage newLanguage)
    {
        language = newLanguage;
        UpdateText();
    }

    private void UpdateText()
    {
        if (text != null)
        {
            text.text = TranslationManager.Instance.GetText(category, variabels, language.ToString());
        }

        if (textUI != null)
        {
            textUI.text = TranslationManager.Instance.GetText(category, variabels, language.ToString());
        }
    }
}
