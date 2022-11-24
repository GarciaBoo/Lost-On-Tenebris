using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EndSequence : MonoBehaviour
{
    public List<TMP_Text> text;
    public float fadeTime = 1.0f;
    public float fadeInterval = 0.3f;
    public float firstFadeTime = 5.0f;
    
    private void Start()
    {
        var seq = DOTween.Sequence();

        seq.AppendInterval(firstFadeTime);
        for (int i = 0; i < text.Count; i++)
        {
            seq.Append(text[i].DOFade(1.0f, fadeTime));
            seq.AppendInterval(fadeInterval);
        }

        seq.Play();
    }
}
