using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    [SerializeField] private bool stopTime;
    [SerializeField] private AnimationClip openAnim;
    [SerializeField] private AnimationClip closeAnim;
    [SerializeField] private bool anyKeyDownCloses;
    [SerializeField, Range(0.0f, 10.0f)] private float timeToRead;
    private Animation anim;
    [SerializeField] bool startOpen;
    private bool openState;
    private CanvasGroup canvasGroup;


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        anim = GetComponent<Animation>();


        if (anim.GetClip(openAnim.name) == null)
        {
            anim.AddClip(openAnim, openAnim.name);
        }

        if (anim.GetClip(closeAnim.name) == null)
        {
            anim.AddClip(closeAnim, closeAnim.name);
        }
    }

    private void Start()
    {
        if (startOpen)
        {
            Open();
        }
    }
    void Update()
    {
        if (anyKeyDownCloses && openState && Input.anyKey)
        {
            Close();
        }
    }

    public void Open()
    {
        canvasGroup.interactable = false;
        anim.Play(openAnim.name);
        StartCoroutine(OpenCorutine());
    }

    IEnumerator OpenCorutine()
    {
        yield return new WaitForSeconds(openAnim.length);

        openState = true;
        canvasGroup.interactable = true;
        if (stopTime)
        {
            Time.timeScale = 0;
        }

        if (timeToRead > 0)
        {
            openState = false;
            StartCoroutine(WaitToUnlockClosing());
        }
    }

    IEnumerator WaitToUnlockClosing()
    {
        yield return new WaitForSeconds(timeToRead);
        openState = true;
    }

    public void Close()
    {
        canvasGroup.interactable = false;
        anim.Play(closeAnim.name);
        if (stopTime)
        {
            Time.timeScale = 1;
        }

        openState = false;
    }
}
