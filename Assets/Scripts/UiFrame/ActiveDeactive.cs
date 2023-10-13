using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDeactive : MonoBehaviour
{
    [SerializeField] bool onEnableActiveDeactive = true;

    [SerializeField] GameObject[] deactive;
    [SerializeField] GameObject[] active;

    void OnEnable()
    {
        if (onEnableActiveDeactive)
        {
            Active();
            Deactive();
        }
    }

    public void Active()
    {
        foreach (GameObject obj in active)
        {
            obj.SetActive(true);
        }
    }

    public void Deactive()
    {
        foreach (GameObject obj in deactive)
        {
            obj.SetActive(false);
        }
    }
}
