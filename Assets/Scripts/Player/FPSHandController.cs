using FMODUnity;
using LostOnTenebris;
using UnityEngine;

public class FPSHandController : MonoBehaviour
{
    [SerializeField] private Animator arms;
    [SerializeField] private Animator left;
    [SerializeField] private Animator right;

    [SerializeField] private FPSController player;
    
    public EventReference panelOpen;
    private static readonly int speed = Animator.StringToHash("Speed");
    private static readonly int abrirPainel = Animator.StringToHash("AbrirPainel");

    private void Update()
    {
        if (!player.Moving)
            arms.SetInteger(speed, 0);
        else if (!player.Running)
            arms.SetInteger(speed, 1);
        else
            arms.SetInteger(speed, 2);
    }

    public void UseFlashlight()
    {
        left.SetTrigger(abrirPainel);
        right.SetTrigger(abrirPainel);
        RuntimeManager.PlayOneShot(panelOpen);
    }
}
