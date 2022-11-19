using System;
using FMODUnity;
using LostOnTenebris;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeathSequence : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool canDie;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
 
    [Header("References")]
    [SerializeField] private FloatVariable charge;
    [SerializeField] private StudioEventEmitter deathSound;
    private bool dead;
    
    private void Update()
    {
        if (charge.value <= 0.0f && canDie && !dead)
        {
            dead = true;
            RenderSettings.ambientLight = Color.black;
            Invoke("Die", Random.Range(minTime, maxTime));
        }
    }

    private void Die()
    {
        deathSound.Play();
    }
}
