using System;
using LostOnTenebris;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    public FloatVariable brightness;
    public Scrollbar scroll;

    private void Start()
    {
        scroll.onValueChanged.AddListener(SetBrightness);
        scroll.value = brightness.value;
    }

    private void SetBrightness(float value)
    {
        brightness.value = value;
    }
}
