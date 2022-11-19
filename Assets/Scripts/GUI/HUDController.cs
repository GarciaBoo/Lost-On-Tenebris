using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    // Alert
    [Header("Alert references")]
    [SerializeField] private TMP_Text alert_text;
    [SerializeField] private Image alert_background;
    
    // Prompt
    [Header("Prompt references")]
    [SerializeField] private TMP_Text prompt_key_gamepad;
    [SerializeField] private TMP_Text prompt_key_keyboard;
    [SerializeField] private TMP_Text prompt_key_prompt;
    
    // References
    [Header("Player references")]
    [SerializeField] private PlayerInput playerInput;

    public void Alert(string text)
    {
        text = text.Replace("\\n", "\n");
        text = text.Replace("\\t", "\t");
        alert_text.text = text;

        var alert_text_sequence = DOTween.Sequence();
        var alert_background_sequence = DOTween.Sequence();

        alert_text_sequence.Append(alert_text.DOFade(1.0f, 1.0f))
            .AppendInterval(5.0f)
            .Append(alert_text.DOFade(0.0f, 1.0f));
        alert_background_sequence.Append(alert_background.DOFade(1.0f, 1.0f))
            .AppendInterval(5.0f)
            .Append(alert_background.DOFade(0.0f, 1.0f));

        alert_text_sequence.Play();
        alert_background_sequence.Play();
    }

    public void SetPrompt(string text)
    {
        switch (playerInput.currentControlScheme)
        {
            case "KeyboardMouse":
                prompt_key_gamepad.gameObject.SetActive(false);
                prompt_key_keyboard.gameObject.SetActive(true);
                break;
            case "Gamepad":
                prompt_key_gamepad.gameObject.SetActive(true);
                prompt_key_keyboard.gameObject.SetActive(false);
                break;
        }
        
        prompt_key_prompt.text = text;
        prompt_key_prompt.gameObject.SetActive(true);
    }

    public void ClearPrompt()
    {
        prompt_key_gamepad.gameObject.SetActive(false);
        prompt_key_keyboard.gameObject.SetActive(false);
        prompt_key_prompt.gameObject.SetActive(false);
    }
    
}
