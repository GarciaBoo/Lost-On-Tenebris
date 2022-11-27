using System;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using UnityEngine;

public class CutsceneStart : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private List<MonoBehaviour> setActiveAfterAnimation;

    [SerializeField] private Animator capsule;
    [SerializeField] private Animator player;
    [SerializeField] private StudioEventEmitter cutscene;

    private Sequence seq;
    
    private void Start()
    {
        seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            cutscene.Play();
        });
        seq.AppendInterval(2.5f);
        seq.AppendCallback(() =>
        {
            capsule.SetTrigger("Start");
            player.SetTrigger("Start");
        });
        seq.AppendInterval(8.0f);
        seq.AppendCallback(SetAllActive);

        StartCutscene();
    }

    public void StartCutscene()
    {
        seq.Play();
    }
    
    private void SetAllActive()
    {
        player.enabled = false;
        
        characterController.enabled = true;
        for (int i = 0; i < setActiveAfterAnimation.Count; i++)
        {
            setActiveAfterAnimation[i].enabled = true;
        }
        this.enabled = false;
    }
}
