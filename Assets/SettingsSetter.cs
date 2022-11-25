using LostOnTenebris;
using UnityEngine;

public class SettingsSetter : MonoBehaviour
{
    [SerializeField] private FloatVariable brightness;
    [SerializeField] private FloatVariable sensitivity;
    [SerializeField] private FPSController controller;
    [SerializeField] private float sensitivityMultiplier;
    
    private void Start()
    {
        RenderSettings.ambientLight = new Color(brightness.value, brightness.value, brightness.value);
        controller.SetSensitivity(sensitivity.value * sensitivityMultiplier);
    }
}
