using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Transactions;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] TextMeshProUGUI textMeshProUI;
    [SerializeField] float timeTarget;
    [SerializeField] bool start;
    [SerializeField] UnityEvent finishEvent;
    float timer;

    private void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;

            if (textMeshPro != null)
            {
                textMeshPro.text = timer.ToString("f2");
            }

            if (textMeshProUI != null)
            {
                textMeshProUI.text = timer.ToString("f2");
            }

            if (timer >= timeTarget)
            {
                finishEvent.Invoke();

                if (textMeshPro != null)
                {
                    textMeshPro.text = timeTarget.ToString("f2");
                }

                if (textMeshProUI != null)
                {
                    textMeshProUI.text = timeTarget.ToString("f2");
                }
            }
        }
    }

    public void StartTimer()
    {
        start = true;
    }
}
