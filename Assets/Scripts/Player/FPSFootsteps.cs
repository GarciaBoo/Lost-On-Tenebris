using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using LostOnTenebris;
using UnityEngine;

public class FPSFootsteps : MonoBehaviour
{
    [SerializeField] private FPSController player;
    
    [Header("FMOD Settings")]
    [SerializeField] private EventReference footstepEvent;
    [SerializeField] private string speedParameterName;

    [Header("Playback settings")]
    [SerializeField] private float stepDistance = 2.0f;
    
    private Vector3 previousPosition;
    private float distanceTravelled;
    private int speed = 0;

    private void Start()
    {
        EventInstance footstep = RuntimeManager.CreateInstance(footstepEvent);
        RuntimeManager.AttachInstanceToGameObject(footstep, player.transform);
    }

    private void Update()
    {
        if (!player.Grounded)
        {
            distanceTravelled = 0.0f;
            return;
        }
        
        distanceTravelled += (player.Position - previousPosition).magnitude;

        if (distanceTravelled >= stepDistance)
        {
            UpdateSpeed();
            PlayFootstep();
            distanceTravelled = 0.0f;
        }

        previousPosition = player.Position;
    }

    private void UpdateSpeed()
    {
        speed = player.Running ? 1 : 0;
    }
    
    private void PlayFootstep()
    {
        EventInstance footstep = RuntimeManager.CreateInstance(footstepEvent);
        RuntimeManager.AttachInstanceToGameObject(footstep, player.transform);
        footstep.setParameterByName(speedParameterName, speed);
        footstep.start();
        footstep.release();
    }
    
}
