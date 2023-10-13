using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DisplayPanelsSequence
{
    public GameObject[] panels;
    public int activePanelIndex { get; private set; } = 0;

    public DisplayPanelsSequence(GameObject[] panels)
    {
        this.panels = panels;
        activePanelIndex = 0;
    }

    public void ChangePanel(int newIndex)
    {
        if (newIndex >= panels.Length || newIndex < 0)
        {
            Debug.LogError("Panel index below zero or over lenght of panel array, panel index: " + newIndex);
        }
        else
        {
            panels[activePanelIndex].SetActive(false);
            panels[newIndex].SetActive(true);
            activePanelIndex = newIndex;
        }
    }
}

public class DisplayControllerUI : MonoBehaviour
{
    public static DisplayControllerUI Instance;
    public DisplayPanelsSequence[] displayPanelsSequences;

    private void Awake()
    {
        Instance = this;
    }

    public void NextPanel(int displayIndex)
    {
        if (displayIndex < 0 || displayIndex >= displayPanelsSequences.Length)
        {
            Debug.LogError("No display panel with given index");
        }
        else
        {
            displayPanelsSequences[displayIndex].ChangePanel(displayPanelsSequences[displayIndex].activePanelIndex + 1);
        }
    }

    //Metoda dla aplikacji z jednym ekranem
    public void NextPanel()
    {
        if (displayPanelsSequences.Length != 1)
        {
            Debug.LogError("This Method is for Only one display, check if you have displayPanelsSequences created or use NextPanel(int displayIndex)");
        }
        else
        {
            NextPanel(0);
        }
    }

    public void SetPanel(int displayIndex, int panelIndex)
    {
        if (displayIndex < 0 || displayIndex >= displayPanelsSequences.Length)
        {
            Debug.LogError("No display panel with given index");
        }
        else
        {
            displayPanelsSequences[displayIndex].ChangePanel(panelIndex);
        }
    }

    //Metoda dla aplikacji z jednym ekranem
    public void SetPanel(int panelIndex)
    {
        if (displayPanelsSequences.Length != 1)
        {
            Debug.LogError("This Method is for Only one display, check if you have displayPanelsSequences created or use NextPanel(int displayIndex)");
        }
        else
        {
            SetPanel(0, panelIndex);
        }
    }

    public void SetLastPanel(int displayIndex)
    {
        if (displayIndex < 0 || displayIndex >= displayPanelsSequences.Length)
        {
            Debug.LogError("No display panel with given index");
        }
        else
        {
            displayPanelsSequences[displayIndex].ChangePanel(displayPanelsSequences[displayIndex].panels.Length - 1);
        }
    }

    public void RestetAllDisplays()
    {
        for (int i = 0; i < displayPanelsSequences.Length; i++)
        {
            SetPanel(i, 0);
        }
    }

    public void NextPanelWithDelay(int displayIndex, float delay)
    {
        StartCoroutine(DelayNextPanel(displayIndex, delay));
    }

    public void NextPanelWithDelay(float delay)
    {
        StartCoroutine(DelayNextPanel(0, delay));
    }

    public void SetPanelWithDelay1Sec(int displayIndex)
    {
        StartCoroutine(DelaySetPanel(displayIndex, 1f));
    }

    IEnumerator DelayNextPanel(int displayIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        NextPanel(displayIndex);
    }

    IEnumerator DelaySetPanel(int panelIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetPanel(panelIndex);
    }
}

