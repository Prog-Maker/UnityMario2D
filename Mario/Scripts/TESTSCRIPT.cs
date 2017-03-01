using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TESTSCRIPT : MonoBehaviour
{

    public int v = 10;
    public object obj = "String";

    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(() => OnKlick(v, obj));
    }

    private void OnKlick(int v, object obj)
    {
        Debug.Log(obj + " = " + v);
    }
}