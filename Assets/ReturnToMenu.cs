using System;
using System.Collections;
using System.Collections.Generic;
using LostOnTenebris;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public FloatVariable brightness;
    
    private void Start()
    {
        RenderSettings.ambientLight = new Color(brightness.value, brightness.value, brightness.value);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(1);
        }
    }
}
