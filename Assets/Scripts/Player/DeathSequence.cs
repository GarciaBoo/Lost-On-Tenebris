using System;
using FMODUnity;
using LostOnTenebris;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] private FPSController controller;
    [SerializeField] private RectTransform deathScreen;
    [SerializeField] private int menuSceneIndex = 1; 

    private void Update()
    {
        if (charge.value <= 0.0f && canDie && !dead)
        {
            dead = true;
            RenderSettings.ambientLight = Color.black;
            float time = Random.Range(minTime, maxTime);
            deathScreen.gameObject.SetActive(true);
            Invoke("Die", time);
            Invoke("RedirectToMenu", time + 12.5f);
        }
    }

    private void Die()
    {
        controller.Pause(true);
        deathSound.Play();
    }

    private void RedirectToMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }
}
